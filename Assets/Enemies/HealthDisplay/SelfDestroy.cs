using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{

    [SerializeField] private float _destroyTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,_destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
