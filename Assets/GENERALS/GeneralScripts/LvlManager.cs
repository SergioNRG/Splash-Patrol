using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LvlManager : MonoBehaviour
{
    public static LvlManager instance;

    [SerializeField] private GameObject _loadCanvas;
    [SerializeField] private Slider _loadBar;

    private float _target;
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
        _loadBar.value = Mathf.MoveTowards(_loadBar.value,_target, 3 * Time.deltaTime);
    }

    public async void LoadScene(string sceneName)
    {
       
        _target = 0;
        _loadBar.value= 0;
        
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loadCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            _target = scene.progress;

        } while (scene.progress < 0.9f);


        scene.allowSceneActivation = true;
        _loadCanvas.SetActive(false);
    }
}
