using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject _backGround;
    [SerializeField] private List<Sprite> _bGImages;

    public static LoadingScreen instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

    }
    private void OnEnable()
    {
        var sprite = _backGround.GetComponent<Image>();
        int num = Random.Range(0, _bGImages.Count);
        sprite.sprite = _bGImages[num];
    }
}
