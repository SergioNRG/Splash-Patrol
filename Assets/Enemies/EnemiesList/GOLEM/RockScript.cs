using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class RockScript : MonoBehaviour, IPooled
{
    [SerializeField] private float _rotationSpeed = 200f;
    [SerializeField] private int _rockDamage;
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
            if (_rockDamage != 0)
            {
                other.GetComponent<IDamageable>().ApplyDamage(_rockDamage);
            }
            //gameObject.SetActive(false);
            //_dragonBulletPool.Release(gameObject);
        }
        gameObject.SetActive(false);
        _golemRocksPool.Release(gameObject);

    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        _golemRocksPool = pool;
    }

}
