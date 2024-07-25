using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sound Prefab")]
    [SerializeField] private AudioSource _soundObjectPrefab;

    [Header("Sounds")]
    [SerializeField] private AudioClip _soundOfRecord;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private AudioClip _lvlPassSound;

    private Coroutine _soundPlayCoroutine;

    public static SoundManager instance;


    public AudioClip SoundOfRecord {  get { return _soundOfRecord; } }
    public AudioClip GameOverSound { get { return _gameOverSound; } }
    public AudioClip HurtSound { get { return _hurtSound; } }
    public AudioClip LvlPassSound { get { return _lvlPassSound; } }
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
