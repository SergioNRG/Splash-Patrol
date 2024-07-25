using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameOverMenuManager : MonoBehaviour
{

    [Header("Sounds")]
    [SerializeField] private AudioClip _gameOverSound;

    private void Start()
    {
        SavesAndLoads.Instance.SaveRecord();
        UIManager.instance.ActivateBloodEffect();
        SoundManager.instance.PlayFXSound(_gameOverSound,0.05f);
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
