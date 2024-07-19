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

    private int _nextSceneIndex;
    private string _sceneName;
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
        
        scene.allowSceneActivation = true;
      
        await Task.Delay(100);
        _loadCanvas.SetActive(false);
       
        _nextSceneIndex = SceneManager.GetActiveScene().buildIndex +1;
        Debug.Log(_nextSceneIndex);
    }

    public void LoadSceneLVL()
    {
        int loadSceneIndex = _nextSceneIndex;
        string pathToScene = SceneUtility.GetScenePathByBuildIndex(_nextSceneIndex);
        _sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
        Debug.Log(_sceneName);
        LoadSceneByName(_sceneName);
    }
}
