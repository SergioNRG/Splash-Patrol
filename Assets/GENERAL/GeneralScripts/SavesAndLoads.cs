using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SavesAndLoads : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _nameTMPro;



    public static SavesAndLoads Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        LoadRecord();
    }


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("PlayerName", null);
        PlayerPrefs.SetString("PlayerName",_nameTMPro.text.ToString());
    }

    public void SaveRecord()
    {
        int score = ScoreManager.Instance.GetScore();
        if (score >= PlayerPrefs.GetInt("Record"))
        {
            PlayerPrefs.SetInt("Record", score);
            PlayerPrefs.SetString("RecordName", PlayerPrefs.GetString("PlayerName"));
            SoundManager.instance.PlayFXSound(SoundManager.instance.SoundOfRecord, 0.03f);
        }else { SoundManager.instance.PlayFXSound(SoundManager.instance.GameOverSound, 0.05f); }

        Debug.Log("o record é "+PlayerPrefs.GetString("RecordName") +" " + PlayerPrefs.GetInt("Record"));
        ScoreManager.Instance.ResetScore();
    }

    public int LoadRecord()
    {
       return PlayerPrefs.GetInt("Record");
    }

    public string LoadRecordName()
    {
       return PlayerPrefs.GetString("RecordName");
    }
}
