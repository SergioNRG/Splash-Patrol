using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UIManager : MonoBehaviour
{
    [Header("Screen effects time")]
    [SerializeField] private float _hurtDisplayTime;
    [SerializeField] private float _hurtFadeTime;

    [Header("Effects Intensity")]
    [SerializeField] private float _voronoiIntensityStartAmount = 2.5f;
    [SerializeField] private float _vignetteIntensityStartAmount = 1.5f;

    [Header("FullScreen References")]
    [SerializeField] private ScriptableRendererFeature _fullScreenDamage;
    [SerializeField] private Material _material;

    [Header("UI References")]
    [SerializeField] private GameObject _startButton;
    [SerializeField] private TextMeshProUGUI _recordName;
    [SerializeField] private TextMeshProUGUI _recordScore;

    private int _voronoiIntensity = Shader.PropertyToID("_VoronoiIntensity");
    private int _vignetteIntensity = Shader.PropertyToID("_VignetteIntensity");

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _startButton.SetActive(false);
        ActivateBloodEffect();
        _recordName.text = SavesAndLoads.Instance.LoadRecordName();
        _recordScore.text = SavesAndLoads.Instance.LoadRecord().ToString();
    }



    public void ActivateBloodEffect()
    {
        _fullScreenDamage.SetActive(true);
        _material.SetFloat(_voronoiIntensity, _voronoiIntensityStartAmount);
        _material.SetFloat(_vignetteIntensity, _vignetteIntensityStartAmount);
    }

    public void DeactivateBloodEffect()
    {
        _fullScreenDamage.SetActive(false);
    }

    public IEnumerator DamageEffect()
    {
        ActivateBloodEffect();

        yield return new WaitForSeconds(_hurtDisplayTime);

        float elapsedTime = 0f;
        while (elapsedTime < _hurtFadeTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpVoronoi = Mathf.Lerp(_voronoiIntensityStartAmount, 0f, (elapsedTime / _hurtFadeTime));
            float lerpVignette = Mathf.Lerp(_vignetteIntensityStartAmount, 0f, (elapsedTime / _hurtFadeTime));

            _material.SetFloat(_voronoiIntensity, lerpVoronoi);
            _material.SetFloat(_vignetteIntensity, lerpVignette);

            yield return null;
        }

        _fullScreenDamage.SetActive(false);
    }

    public void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DeactivateCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ActivatePanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void DeactivatePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void ActivateAndDeactivatePanel(GameObject panel)
    {
        if (!panel.activeInHierarchy)
        {
            panel.SetActive(true);
        }
        else { panel.SetActive(false); }
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

   
}
