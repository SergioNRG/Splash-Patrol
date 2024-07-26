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

    public async void LoadSceneByName(string sceneName)
    {

        _loadBar.value= 0;
        var cam = Camera.main;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        if (cam != null) { cam.gameObject.SetActive(false); } 
        _loadCanvas.SetActive(true);

        do
        {
            
            await Task.Delay(100);
            _loadBar.value = scene.progress;

        } while (scene.progress < 0.9f);



        scene.allowSceneActivation = true;
      
        await Task.Delay(100);
        _loadCanvas.SetActive(false);

        if (cam != null) { cam.gameObject.SetActive(true); }
        _nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void LoadSceneLVL()
    {
        int loadSceneIndex = _nextSceneIndex;
        string pathToScene = SceneUtility.GetScenePathByBuildIndex(_nextSceneIndex);
        _sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
        LoadSceneByName(_sceneName);
    }
}
