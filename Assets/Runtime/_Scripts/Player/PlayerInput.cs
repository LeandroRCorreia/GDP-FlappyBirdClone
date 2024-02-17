using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public bool Tap()
    {
        bool isTapped = false;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isTapped = true;
        }
        if(Input.touchCount > 0)
        {
            isTapped = IsTouch();

        }

        return isTapped;
    }

    private bool IsTouch()
    {
        Touch[] touches = Input.touches;
        bool isFlapp = false;

        foreach (var touch in touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                isFlapp = true;
            }

        }

        return isFlapp;
    }
    
}
