using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameOverMenuManager : MonoBehaviour
{
    /*[Header("Effects Intensity")]
    [SerializeField] private float _voronoiIntensityStartAmount = 2.5f;
    [SerializeField] private float _vignetteIntensityStartAmount = 1.5f;

    [Header("References")]
    [SerializeField] private ScriptableRendererFeature _fullScreenDamage;
    [SerializeField] private Material _material;

    private int _voronoiIntensity = Shader.PropertyToID("_VoronoiIntensity");
    private int _vignetteIntensity = Shader.PropertyToID("_VignetteIntensity");*/
    [Header("Sounds")]
    [SerializeField] private AudioClip _gameOverSound;

    private void Start()
    {
        SavesAndLoads.Instance.SaveRecord();
       /* _fullScreenDamage.SetActive(true);
        _material.SetFloat(_voronoiIntensity, _voronoiIntensityStartAmount);
        _material.SetFloat(_vignetteIntensity, _vignetteIntensityStartAmount);*/
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
