using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class BossRockScript : MonoBehaviour,IPooled
{
    private Transform _playerTransform;
    private Rigidbody _rb;
    private float _timer = 0;
    private bool _isCounting;
    [SerializeField] private GameObject _enemyToSpawn;
    //private float _force;
    //private GameObject _refer;
    [SerializeField] private float _rotationSpeed = 200f;

    private ObjectPool<GameObject> _bossRocksPool;
    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
       // _rb = GetComponent<Rigidbody>();
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
            Debug.Log("yuuup");
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
            //Instantiate(_enemyToSpawn,transform.position, Quaternion.identity);
            _bossRocksPool.Release(gameObject);
        }                
    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        _bossRocksPool = pool;        
    }

   /* public void SetProjectileForce(float force, GameObject reference)
    {
        _force = force;
        _refer = reference;
    }*/
}
