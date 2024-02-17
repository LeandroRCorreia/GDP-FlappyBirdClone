using UnityEngine;

public class PassPipeTrigger : MonoBehaviour
{
    public void PassPipe(GameMode gameMode)
    {
        gameMode.OnPassedPipe();
    }


}
