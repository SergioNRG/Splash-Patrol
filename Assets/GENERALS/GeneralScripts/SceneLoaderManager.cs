using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class SceneLoaderManager : MonoBehaviour
{
    public static SceneLoaderManager instance;

    [SerializeField] private GameObject _loadCanvas;
    [SerializeField] private Slider _loadBar;

    public int ActiveSceneIndex;
    //private float _target;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
       // _loadCanvas.SetActive(true);
    }


    void Update()
    {
       // _loadBar.value = Mathf.MoveTowards(_loadBar.value,_target, 1.5f* Time.deltaTime);
    }

    public async void LoadSceneByName(string sceneName)
    {
       
        //_target = 0;
        _loadBar.value= 0;
        
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loadCanvas.SetActive(true);

        do
        {
            
            await Task.Delay(100);
            //_target = scene.progress;
            _loadBar.value = scene.progress;

        } while (scene.progress < 0.9f);

        ActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(_loadBar.value);  
        scene.allowSceneActivation = true;
        await Task.Delay(100);
        _loadCanvas.SetActive(false);

    }

    public void LoadSceneLVL()
    {
        int loadSceneIndex = ActiveSceneIndex++;
        string sceneName = SceneManager.GetSceneByBuildIndex(loadSceneIndex).name;
        LoadSceneByName(sceneName);
    }
}
