using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerEffectsManager : MonoBehaviour
{

    [SerializeField] private PlayerHealthManager _healthManager;// just to see

    [Header("Screen effects time")]
    [SerializeField] private float _hurtDisplayTime;
    [SerializeField] private float _hurtFadeTime;
   

    [Header("Effects Intensity")]
    [SerializeField] private float _voronoiIntensityStartAmount = 2.5f;
    [SerializeField] private float _vignetteIntensityStartAmount = 1.25f;


    [Header("Sounds")]
    [SerializeField] private AudioClip _hurtSound;


    [Header("References")]
    [SerializeField] private ScriptableRendererFeature _fullScreenDamage;
    [SerializeField] private Material _material;


    private int _voronoiIntensity = Shader.PropertyToID("_VoronoiIntensity");
    private int _vignetteIntensity = Shader.PropertyToID("_VignetteIntensity");


 
    void Start()
    {
        _healthManager = GetComponent<PlayerHealthManager>();
        _fullScreenDamage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RoarEffect()
    {
        
    }
    public void Idleffect()
    {
       
    }
    public void MoveEffect()
    {
       
    }
    public void ChaseEffect()
    {

    }

    public void AttackEffect()
    {
       
    }

    public void TakeDamageEffect(int damage)
    {
        SoundFXManager.instance.PlayFXSound(_hurtSound, 0.25f);
        StartCoroutine(DamageEffect());
    }

    public void Die(Vector3 position, GameObject pl)
    {
        Debug.Log("player DIE");
        _fullScreenDamage.SetActive(true);
        SceneLoaderManager.instance.LoadSceneByName("GameOver");

    }


    public void HealEffect(int amount)
    {
        Debug.Log("healing");
    }

    private IEnumerator DamageEffect()
    {
        _fullScreenDamage.SetActive(true);
        _material.SetFloat(_voronoiIntensity,_voronoiIntensityStartAmount);
        _material.SetFloat(_vignetteIntensity,_vignetteIntensityStartAmount);

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
}
