using System.Collections.Generic;
using UnityEngine;

public class EndlessLevelGenerator : MonoBehaviour
{
    [SerializeField] PipeSpawner PipeSpawnerPrefab;
    [SerializeField] Ground[] grounds;
    [SerializeField] Camera mainCamera;

    [Header("Level Generator Params")]
    [SerializeField] private int minPipesFrontOfPlayer;
    [SerializeField] private float distanceBeetwenPipesX = 7f;
    [SerializeField] private float firstInstancePipeDistanceByPlayer;

    [Header("External Dependencies")]
    [SerializeField] private PlayerController player;
    [SerializeField] private GameMode gameMode;

    private List<PipeSpawner> currentLevelInScene = new(10);

    private PipeSpawner LastPipe
    {
        get
        {
            return currentLevelInScene.Count > 0 ? currentLevelInScene[currentLevelInScene.Count - 1] 
            : null;

        }

    }

    private Ground LastGround => grounds[grounds.Length - 1];

    [Header("ObjectPool Parameter")]
    [SerializeField] private ObjetPool<PipeSpawner> pipePool;

    void Awake()
    {
        pipePool.InitializePool();

    }

    private void Update()
    {
        UpdateLevel();

    }

    private void UpdateLevel()
    {
        UpdateEndlessGround();
        if (!gameMode.IsGameStarted) return;
        UpdateEndlessPipe();
    }

    private void UpdateEndlessPipe()
    {
        int lastPipeIndex = -1;
        lastPipeIndex = FindPlayerLastPipeIndex(lastPipeIndex);

        var howManyPipesAreFrontOfPlayer = currentLevelInScene.Count - minPipesFrontOfPlayer;
        if (howManyPipesAreFrontOfPlayer < minPipesFrontOfPlayer) SpawnLevels(1);

        RemovePipesBehindPlayer(lastPipeIndex);

    }

    private void UpdateEndlessGround()
    {
        var passedMinGround = player.transform.position.x > grounds[0].Sprite.bounds.min.x;
        if(passedMinGround && !IsRendererVisible(grounds[0].Sprite))    
        {
            var lastGroundAux = LastGround;
            var firstGround = grounds[0];

            var firstGroundPosition = firstGround.transform.position;
            firstGroundPosition.x = LastGround.transform.position.x + LastGround.Sprite.bounds.size.x;
            firstGround.transform.position = firstGroundPosition;

            grounds[grounds.Length - 1] = firstGround;
            grounds[0] = lastGroundAux;

        }

    }

    private int FindPlayerLastPipeIndex(int playerIndex)
    {
        for (int i = 0; i < currentLevelInScene.Count; i++)
        {
            var playerPositionX = player.transform.position.x;
            var pipeSpawnerPos = currentLevelInScene[i].transform.position;
            if (playerPositionX > pipeSpawnerPos.x && !IsRendererVisible(currentLevelInScene[i].TopPipe.Sprite))
            {
                playerIndex = i;
                break;
            }

        }

        return playerIndex;
    }

    private bool IsRendererVisible(SpriteRenderer sprite)
    {
        return IsBoxVisibleXOnly(sprite.bounds.center, sprite.bounds.size.x);
    }

    private bool IsBoxVisibleXOnly(Vector3 center, float width)
    {
        var right = center + 0.5f * width * Vector3.right;
        var left = center - 0.5f * width * Vector3.right;

        float rightX = mainCamera.WorldToViewportPoint(right).x;
        float leftX = mainCamera.WorldToViewportPoint(left).x;

        return !(leftX > 1 || rightX < 0);
    }

    private void RemovePipesBehindPlayer(int playerIndex)
    {
        for (int i = 0; i < playerIndex + 1; i++)
        {
            pipePool.ReturnToPool(currentLevelInScene[i]);
        }
        currentLevelInScene.RemoveRange(0, playerIndex + 1);

    }

    private void SpawnLevels(int levelQuantity)
    {
        for(int i = 0; i < levelQuantity; i++)
        {
            SpawnPipe();
        }

    }

    private void SpawnPipe()
    {
        Vector3 positionToActive;
        
        if(LastPipe != null)
        {
            positionToActive = PositioningCasualPipes();
        }
        else
        {
            positionToActive = PositioningFirstPipeX();
        }

        PipeSpawner pipeInstance = pipePool.GetFromPoolOrCreate(positionToActive, Quaternion.identity, transform);

        currentLevelInScene.Add(pipeInstance);

        Vector3 PositioningFirstPipeX()
        {
            var position = transform.position;
            position.x = player.transform.position.x + firstInstancePipeDistanceByPlayer;

            return position;
        }

        Vector3 PositioningCasualPipes()
        {
            Vector3 positionToActive;
            float point = LastPipe.transform.position.x + distanceBeetwenPipesX;
            positionToActive = new Vector3(point, 0);
            return positionToActive;
        }
        
    }

}
