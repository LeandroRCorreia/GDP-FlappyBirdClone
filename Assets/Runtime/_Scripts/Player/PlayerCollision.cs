using UnityEngine;

public struct IPlayerInfo
{
    public PlayerController playerController;

}

[RequireComponent(typeof(PlayerController), typeof(PlayerAnimationController))]
public class PlayerCollision : MonoBehaviour
{
    [Header("External Dependencies")]
    [SerializeField] private GameMode gameMode;
    [SerializeField] private ScreenController screenController;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.TryGetComponent(out PassPipeTrigger pipeTrigger))
        {
            pipeTrigger.PassPipe(gameMode);
        }
        
    }

}
