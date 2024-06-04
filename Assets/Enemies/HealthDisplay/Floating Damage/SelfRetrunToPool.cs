using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Pool;
using UnityEngine;

public class SelfReturnToPool : MonoBehaviour
{

    [SerializeField] private float _displayTime = 0.5f;

    private ObjectPool<GameObject> _pool;
    // Start is called before the first frame update
    void Start()
    {
       // Destroy(gameObject,_destroyTime);
    }

    private void Update()
    {
        _displayTime -= Time.deltaTime;
        if ( _displayTime <= 0)
        {
            _displayTime = 0.5f;
            gameObject.SetActive(false);
            _pool.Release(gameObject);
        }
        
    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        _pool = pool;
    }
}
