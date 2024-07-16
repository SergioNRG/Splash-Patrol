using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LVLPortal : MonoBehaviour
{

    [SerializeField] private Image _lvlImage;
    [SerializeField] private List<Sprite> _lvlSprites;
    // Start is called before the first frame update
    void Start()
    {
        _lvlImage = GetComponentInChildren<Image>();
        //_lvlImage = GetComponent<Image>();
        if (_lvlImage != null)
        {
            if (EnemySpawner.instance != null)
            {
                _lvlImage.sprite = _lvlSprites[EnemySpawner.instance.lvl-1];
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        _lvlImage.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

    private void OnEnable()
    {
        if (_lvlImage != null)
        {
            if(EnemySpawner.instance != null)
            {
                _lvlImage.sprite = _lvlSprites[EnemySpawner.instance.lvl-1];
            }
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // fazer load do next lvl
            Debug.Log("entrou mudar lvl");
            SceneLoaderManager.instance.LoadSceneLVL();
        }
    }
}
