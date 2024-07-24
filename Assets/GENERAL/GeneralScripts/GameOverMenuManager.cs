using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuManager : MonoBehaviour
{
    public void LoadIntro()
    {
        SceneLoaderManager.instance.LoadSceneByName("INTRO");
    }

    public void LoadMainMenu()
    {
        SceneLoaderManager.instance.LoadSceneByName("MAINMENU");
    }
}
