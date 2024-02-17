using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private UiScreen[] uiScreens;
    [SerializeField] private UiScreen CurrentScreen;


    private void Start()
    {
        CloseAllScreens();
        
    }

    public void ShowScreen<T>()
    {
        var screenToShow = FindScreenByType<T>();

        if(CurrentScreen != screenToShow)
        {
            if (CurrentScreen != null) CurrentScreen.gameObject.SetActive(false);
            CurrentScreen = screenToShow;
            CurrentScreen.gameObject.SetActive(true);
        }

    }

    private UiScreen FindScreenByType<T>()
    {
        for (int i = 0; i < uiScreens.Length; i++)
        {
            if(typeof(T) == uiScreens[i].GetType())
            {
                return uiScreens[i];
            }

        }
        Debug.LogError("i cant find that screenType: " +  typeof(T).ToString());
        return null;
    }

    private void CloseAllScreens()
    {
        for (int i = 0; i < uiScreens.Length; i++)
        {
            uiScreens[i].gameObject.SetActive(false);

        }

        CurrentScreen = null;
    }

}
