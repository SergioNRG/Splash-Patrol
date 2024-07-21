using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro.EditorUtilities;

public class LVLPortal : MonoBehaviour
{

    [SerializeField] private Image _lvlImage;
    [SerializeField] private List<Sprite> _lvlSprites;

    private Camera _cam;
    private void Awake()
    {
        _cam = Camera.main;
    }
    void Start()
    {
        _lvlImage = GetComponentInChildren<Image>();
        /*if(EnemySpawner.instance != null)
        {
            gameObject.SetActive(false);
        }*/
        
        //_lvlImage = GetComponent<Image>();
        if (_lvlImage != null)
        {
            if (EnemySpawner.instance != null)
            {
                _lvlImage.sprite = _lvlSprites[GameManager.Instance.lvl-1];
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //_lvlImage.transform.LookAt(Camera.main.transform.position, Vector3.up);
        _lvlImage.transform.LookAt(_cam.transform.position, Vector3.up);
    }

  /*  private void OnEnable()
    {
        if (_lvlImage != null)
        {
            if(EnemySpawner.instance != null)
            {
                _lvlImage.sprite = _lvlSprites[EnemySpawner.instance.lvl-1];
            }
            
        }
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entrou mudar lvl");
            SceneLoaderManager.instance.LoadSceneLVL();
        }
    }
}
