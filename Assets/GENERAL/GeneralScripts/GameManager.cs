using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _portalLife;

    public int Lvl = 1;
    public static GameManager Instance;

    private int _portalRestartLife;
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

    private void Start()
    {
        _portalRestartLife = _portalLife;
    }

    public void ChangeLvl()
    {
        Lvl++;
        SoundManager.instance.PlayFXSound(SoundManager.instance.LvlPassSound, 0.02f);
    }

    public void ReStart()
    {
        GameManager.Instance.Lvl = 1;
        GameManager.Instance._portalLife = _portalRestartLife;
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
