using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _soundObjectPrefab;

    public static SoundManager instance;

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


    public void PlayFXSound(AudioClip audioClip, float volume)
    {
        //AudioSource audioSource = Instantiate (_soundObjectPrefab, spawnTrans.position,Quaternion.identity) ;
        AudioSource audioSource = SoundFXPooler.instance.TakeFromPool("FXSound").GetComponent<AudioSource>();
        audioSource.gameObject.SetActive(true);


        _soundPlayCoroutine = StartCoroutine(PlayAudioAndDeactivate(audioSource, audioClip, volume));
    }

    private IEnumerator PlayAudioAndDeactivate(AudioSource audioSource,AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length);
        audioSource.gameObject.SetActive(false); 
    }
}
