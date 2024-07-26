using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
        if (_lvlImage != null)
        {
            if (EnemySpawner.instance != null)
            {
                _lvlImage.sprite = _lvlSprites[GameManager.Instance.Lvl-1];
            }

        }
    }

    void Update()
    {
        _lvlImage.transform.LookAt(_cam.transform.position, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneLoaderManager.instance.LoadSceneLVL();
        }
    }
}
