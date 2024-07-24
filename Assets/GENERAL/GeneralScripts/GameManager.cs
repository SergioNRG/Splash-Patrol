using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public int Lvl = 1;

    [SerializeField ]private int EnemiesLeft;

    // maybe do a gameover event
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    public void ChangeLvl()
    {
        Lvl++;
    }

    public void ResetValues()
    {

    }

    public void ChangePortalLife()
    {
        EnemiesLeft--;
        if (EnemiesLeft <= 0) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneLoaderManager.instance.LoadSceneByName("GameOver"); 
        }
      
    }
}
