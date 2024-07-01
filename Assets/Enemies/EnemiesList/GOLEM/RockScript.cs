using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class RockScript : MonoBehaviour, IPooled
{
    [SerializeField] private float _rotationSpeed = 200f;

    private Transform _playerTransform;  
    private ObjectPool<GameObject> _golemRocksPool;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            _golemRocksPool.Release(gameObject);
        }
        else 
        {
            gameObject.SetActive(false);
            _golemRocksPool.Release(gameObject);
        }

    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        _golemRocksPool = pool;
    }

}
