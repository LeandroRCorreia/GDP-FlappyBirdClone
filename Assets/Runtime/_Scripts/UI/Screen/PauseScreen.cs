using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : UiScreen
{   

    [Header("Ui Elements")]
    [SerializeField] private Button resumeButton;


    [Header("External Dependencies")]
    [SerializeField] private GameMode gameMode;
    [SerializeField] private UiAudioController uiAudioController;


    private void Start()
    {
        resumeButton.onClick.AddListener(OnResumeButton);
    }

    private void OnResumeButton()
    {
        gameMode.OnResumeGame();
        uiAudioController.PlayButtonAudio();

    }

}
