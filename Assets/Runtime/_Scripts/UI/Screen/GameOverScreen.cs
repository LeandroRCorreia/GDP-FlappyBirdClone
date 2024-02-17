using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

public class GameOverScreen : UiScreen
{

    [Header("Ui Elements")]

    [SerializeField] private Button restartGameButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private TextMeshProUGUI lastScoreTxt;
    [SerializeField] private TextMeshProUGUI bestScoreTxt;
    [SerializeField] private Image newBestScoreImg;
    [SerializeField] private Image medalImg;

    [Header("External Dependencies")]

    [SerializeField] private GameMode gameMode;
    [SerializeField] private GameSaver gameSaver;
    [SerializeField] private UiAudioController uiAudioController;

    [Header("Container")]
    [SerializeField] private CanvasGroup gameOverImageContainer;
    [SerializeField] private CanvasGroup statsContainer;
    [SerializeField] private CanvasGroup buttonContainer;

    [Header("GameOverImage Tween")]
    [SerializeField] private Transform gameOverImageStart;
    [SerializeField] private float gameOverImageTime = 0.375f;

    [Header("GameOverStatus Tween")]
    [SerializeField] private Transform startStatsContainer;
    [SerializeField] private float statsContainerDelay = 0.25f;
    [SerializeField] private float statsContainerAnimationTime = 1;
    

    [Header("Button Tween")]
    [SerializeField] private float buttonContainerAnimationTime = 1;
    [SerializeField] private float buttonContainerDelay = 0.5f;

    private void Start()
    {
        restartGameButton.onClick.AddListener(OnRestartGameButton);
        exitGameButton.onClick.AddListener(OnExitGameButton);

    }

    private void OnEnable()
    {
        UpdateUi();
        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {

        gameOverImageContainer.alpha = 0;
        gameOverImageContainer.blocksRaycasts = false;

        statsContainer.alpha = 0;
        statsContainer.blocksRaycasts = false;

        buttonContainer.alpha = 0;
        buttonContainer.blocksRaycasts = false;

        //GameOverImage Container
        yield return AnimateCanvasGroup(
            gameOverImageContainer,
            1,
            gameOverImageStart.position,
            gameOverImageContainer.transform.position,
            gameOverImageTime
        );

        yield return new WaitForSeconds(statsContainerDelay);
        //Stats container 
        yield return AnimateCanvasGroup(
            statsContainer,
            1,
            startStatsContainer.transform.position,
            statsContainer.transform.position,
            statsContainerAnimationTime
        );

        yield return new WaitForSeconds(buttonContainerDelay);

        yield return AnimateCanvasGroup(
            buttonContainer,
            1,
            buttonContainer.transform.position,
            buttonContainer.transform.position,
            buttonContainerAnimationTime
        );

        buttonContainer.blocksRaycasts = true;
    }

    private IEnumerator AnimateCanvasGroup(CanvasGroup canvasGroup, float endAlpha, Vector3 from, Vector3 to, float time)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;

        Tween tween = canvasGroup.DOFade(endAlpha, time);

        canvasGroup.transform.position = from;
        canvasGroup.transform.DOMove(to, time);

        yield return tween.WaitForKill();
        canvasGroup.blocksRaycasts = true;

    }

    private void UpdateUi()
    {
        lastScoreTxt.text = gameSaver.CurrentGameData.lastScore.ToString();
        bestScoreTxt.text = gameSaver.CurrentGameData.bestScore.ToString();
        newBestScoreImg.gameObject.SetActive(gameMode.IsNewScore);

        ProcessShowMedal();

    }

    private void ProcessShowMedal()
    {
        Medal medal = gameMode.CalculateMedal();

        if (medal == null)
        {
            medalImg.gameObject.SetActive(false);
        }
        else
        {
            medalImg.sprite = medal.MedalSprite;
            medalImg.gameObject.SetActive(true);
        }
    }

    private void OnRestartGameButton()
    {
        gameMode.OnRestartGame();
        uiAudioController.PlayButtonAudio();

    }

    private void OnExitGameButton()
    {
        gameMode.OnExitGame();
        uiAudioController.PlayButtonAudio();

    }

}
