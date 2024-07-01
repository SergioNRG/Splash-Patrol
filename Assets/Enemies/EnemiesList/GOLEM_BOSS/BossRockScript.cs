using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class BossRockScript : MonoBehaviour,IPooled
{
    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private float _rotationSpeed = 200f;

    private Transform _playerTransform;
    private float _timer = 0;
    private bool _isCounting;
    private ObjectPool<GameObject> _bossRocksPool;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        if (_isCounting) { _timer += Time.deltaTime; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            _bossRocksPool.Release(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        _isCounting = true;
        if (_timer >= 2)
        {
            _timer = 0;           
            gameObject.SetActive(false);
            _bossRocksPool.Release(gameObject);
        }                
    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        _bossRocksPool = pool;        
    }
}
