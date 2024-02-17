using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR

using UnityEditor;

#endif

public class GameMode : MonoBehaviour
{
    [SerializeField] private GameSaver gameSaver;
    [SerializeField] private RewardSettings rewardCalculatorSettings;
    [SerializeField] private float fadeTime = 0.25f;

    [Header("Score")]
    [SerializeField] private int score = 0;

    [Header("Player Movement Params")]
    [SerializeField] private MovementParams waitingStartGameMovement;
    [SerializeField] private MovementParams gamePlayMovement;
    [SerializeField] private MovementParams gameOverMovement;
    [SerializeField] private MovementParams frozenMovement;


    [Header("External Dependencies")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ScreenController screenController;
    [SerializeField] private GameModeAudioController gameModeAudioController;
    [SerializeField] private ScreenEffect screenEffect;

    public bool IsGameStarted {get; private set;} = false;
    public bool IsGameOver {get; private set;} = false;
    public bool IsNewScore {get; private set;} = false;
    public int Score {get => score; private set{score = value;}}


    private void Awake()
    {
        gameSaver.LoadGame();
    }

    private void Start()
    {
        playerController.playerDeathEvent += OnPlayerDeathEvent;
        StartCoroutine(WaitingStartGameCor());

    }

    void OnDestroy()
    {
        playerController.playerDeathEvent -= OnPlayerDeathEvent;
    }

    private IEnumerator WaitingStartGameCor()
    {
        StartCoroutine(screenEffect.FadeOut(fadeTime, Color.black));
        IsGameStarted = false;

        screenController.ShowScreen<WaitingGameplayScreen>();
        playerController.CurrentMovementParams = waitingStartGameMovement;
        //TODO: Make a waitingStartGameAnimation
        while (!playerController.IsFlapped)
        {
            yield return null;
        }
        playerController.CurrentMovementParams = gamePlayMovement;

        screenController.ShowScreen<GameplayScreen>();
        IsGameStarted = true;

    }

    public void OnPauseGame()
    {
        Time.timeScale = 0;
        screenController.ShowScreen<PauseScreen>();

    }

    public void OnResumeGame()
    {
        Time.timeScale = 1;
        screenController.ShowScreen<GameplayScreen>();

    }

    public void OnPassedPipe()
    {
        Score++;
    }

    private void OnPlayerDeathEvent()
    {
        OnGameOver();
    }

    public void OnGameOver()
    {
        if (IsGameOver) return;


        if(!playerController.IsTouchedGround)
        {
            gameModeAudioController.PlayGameOverAudio();
        }

        ProcessSaveGame();
        IsGameOver = true;
        StartCoroutine(screenEffect.Flash());
        screenController.ShowScreen<GameOverScreen>();
        playerController.CurrentMovementParams = gameOverMovement;
    }

    private void ProcessSaveGame()
    {
        IsNewScore = Score > gameSaver.CurrentGameData.bestScore;
        var bestScore = IsNewScore ? Score : gameSaver.CurrentGameData.bestScore;
        SaveGameData saveGameData = new()
        {
            lastScore = Score,
            bestScore = bestScore,

        };
        gameSaver.SaveGame(saveGameData);
    }

    public Medal CalculateMedal()
    {
        return rewardCalculatorSettings.CalculateMedal(Score);
    }

    public void OnRestartGame()
    {
        StartCoroutine(RestartGameCor());
    }
    
    private IEnumerator RestartGameCor()
    {
        yield return screenEffect.FadeOut(fadeTime, Color.black);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void OnExitGame()
    {
        
#if UNITY_EDITOR
        if(EditorApplication.isPlaying) EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }

}
