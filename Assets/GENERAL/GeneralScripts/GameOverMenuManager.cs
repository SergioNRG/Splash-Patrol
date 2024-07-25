using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameOverMenuManager : MonoBehaviour
{

    

    private void Start()
    {        
        UIManager.instance.ActivateBloodEffect();
        SavesAndLoads.Instance.SaveRecord();
    }



    public void LoadIntro()
    {
        SceneLoaderManager.instance.LoadSceneByName("INTRO");
        
    }

    public void LoadMainMenu()
    {
        SceneLoaderManager.instance.LoadSceneByName("MAINMENU");
    }

    public void Exit()
    {
        UIManager.instance.QuitGame();
    }
}
