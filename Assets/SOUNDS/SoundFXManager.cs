using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource _soundObjectPrefab;

    public static SoundFXManager instance;

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
        AudioSource audioSource = Instantiate (_soundObjectPrefab, spawnTrans.position,Quaternion.identity) ;

        audioSource.clip = audioClip ;
        audioSource.volume = volume ;
        audioSource.Play();

        float clipLength = audioSource.clip.length ;

        Destroy (audioSource.gameObject,clipLength ) ;
    }
}
