using UnityEngine;

public class EndGameMenuManager : MonoBehaviour
{
    void Start()
    {
        UIManager.instance.ActivateCursor();
    }

    public void Exit()
    {
        UIManager.instance.QuitGame();
    }
}
