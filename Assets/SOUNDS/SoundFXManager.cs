using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource _soundObjectPrefab;

    public static SoundFXManager instance;

    private Coroutine _soundPlayCoroutine;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject) ; }    
    }


    public void PlayFXSound(AudioClip audioClip, Transform spawnTrans, float volume)
    {
        //AudioSource audioSource = Instantiate (_soundObjectPrefab, spawnTrans.position,Quaternion.identity) ;
        AudioSource audioSource = SoundFXPooler.instance.TakeFromPool("FXSound").GetComponent<AudioSource>();
        Debug.Log(audioSource);
        audioSource.gameObject.SetActive(true);
        if(_soundPlayCoroutine == null)
        {
            _soundPlayCoroutine = StartCoroutine(PlayAudioAndDeactivate(audioSource,audioClip,volume)); 
        }else { StopCoroutine(_soundPlayCoroutine); }
       // audioSource.clip = audioClip ;
       // audioSource.volume = volume ;
       // audioSource.Play();

       // float clipLength = audioSource.clip.length ;
       
       // if (audioSource.time >= clipLength ) { audioSource.gameObject.SetActive (false); }
        
        //Destroy (audioSource.gameObject,clipLength ) ;
    }

    private IEnumerator PlayAudioAndDeactivate(AudioSource audioSource,AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        yield return new WaitUntil(() => audioSource.time >= clip.length);
        audioSource.gameObject.SetActive(false); //Whatever it is that you're wanting to do.
    }
}
