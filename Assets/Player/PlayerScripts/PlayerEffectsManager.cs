using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerEffectsManager : MonoBehaviour
{

    [SerializeField] private PlayerHealthManager _healthManager;// just to see


    void Start()
    {
        _healthManager = GetComponent<PlayerHealthManager>();
       UIManager.instance.DeactivateBloodEffect();
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
        SoundManager.instance.PlayFXSound(SoundManager.instance.HurtSound, 0.25f);
        StartCoroutine(UIManager.instance.DamageEffect());
    }

    public void Die(Vector3 position, GameObject pl)
    {
        Debug.Log("player DIE");
        //_fullScreenDamage.SetActive(true);
        // Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

        UIManager.instance.ActivateCursor();
        GameManager.Instance.ReStart();
        SceneLoaderManager.instance.LoadSceneByName("GameOver");
        
    }


    public void HealEffect(int amount)
    {
        Debug.Log("healing");
    }

 /*   private IEnumerator DamageEffect()
    {
        /*_fullScreenDamage.SetActive(true);
        _material.SetFloat(_voronoiIntensity,_voronoiIntensityStartAmount);
        _material.SetFloat(_vignetteIntensity,_vignetteIntensityStartAmount);

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
    }*/
}
