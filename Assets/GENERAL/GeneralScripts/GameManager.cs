using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _portalLife;

    public int Lvl = 1;
    public static GameManager Instance;


    public int PortalLife {  get { return _portalLife; } }
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
        SoundManager.instance.PlayFXSound(SoundManager.instance.LvlPassSound, 0.02f);
    }

    public void ReStart()
    {
        GameManager.Instance.Lvl = 1;
    }

    public void ChangePortalLife(Vector3 pos, GameObject enemy)
    {
        _portalLife--;
        if (_portalLife <= 0) 
        {
            UIManager.instance.ActivateCursor();
            ReStart();
            SceneLoaderManager.instance.LoadSceneByName("GameOver"); 
        }
      
    }


}
