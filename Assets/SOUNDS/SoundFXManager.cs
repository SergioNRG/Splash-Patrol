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
        //AudioSource audioSource = Instantiate (_soundObjectPrefab, spawnTrans.position,Quaternion.identity) ;
        AudioSource audioSource = SoundFXPooler.instance.TakeFromPool("FXSound").GetComponent<AudioSource>();
        Debug.Log(audioSource);
        audioSource.gameObject.SetActive(true);
        
        audioSource.clip = audioClip ;
        audioSource.volume = volume ;
        audioSource.Play();

        float clipLength = audioSource.clip.length ;
        while (clipLength > 0) 
        {
            audioSource.gameObject.SetActive (true);
        }

        audioSource.gameObject.SetActive(false);
        //Destroy (audioSource.gameObject,clipLength ) ;
    }
}
