using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class BulletScript : MonoBehaviour,IPooled
{
    private Transform _playerTransform;
    private ObjectPool<GameObject> _dragonBulletPool;
    [SerializeField] private int _bulletDamage;
    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_bulletDamage != 0)
            {
                other.GetComponent<IDamageable>().ApplyDamage(_bulletDamage);
            }
            //gameObject.SetActive(false);
            //_dragonBulletPool.Release(gameObject);
        }
        gameObject.SetActive(false);
        _dragonBulletPool.Release(gameObject);
       /* else 
        {
            gameObject.SetActive(false);
            _dragonBulletPool.Release(gameObject);
        }*/
        
    }
    public void SetPool(ObjectPool<GameObject> pool)
    {
        _dragonBulletPool = pool;
    }
}
