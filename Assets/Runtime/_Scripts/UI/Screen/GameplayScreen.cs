using UnityEngine;
using TMPro;
using UnityEngine.UI;

public sealed class GameplayScreen : UiScreen
{

    [Header("Ui Elements")]
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private Button pauseButton;


    [Header("External Dependencies")]
    [SerializeField] private GameMode gameMode;
    [SerializeField] private UiAudioController uiAudioController;

    private void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClick);

    }

    private void LateUpdate()
    {
        scoreTxt.text = $"{gameMode.Score}";
    }


    private void OnPauseButtonClick()
    {
        gameMode.OnPauseGame();
        uiAudioController.PlayButtonAudio();

    }

}
