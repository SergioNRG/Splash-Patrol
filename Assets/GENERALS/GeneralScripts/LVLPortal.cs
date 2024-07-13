using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LVLPortal : MonoBehaviour
{

    [SerializeField] private Image _lvlImage;
    // Start is called before the first frame update
    void Start()
    {
        _lvlImage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _lvlImage.transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
