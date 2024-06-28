using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class BossRockScript : MonoBehaviour,IPooled,IProjectile
{
    private Transform _playerTransform;
    private Rigidbody _rb;
    private float _timer = 0;
    private bool _isCounting;
    [SerializeField] private GameObject _enemyToSpawn;
    private float _force;
    private GameObject _refer;
    [SerializeField] private float _rotationSpeed = 200f;

    private ObjectPool<GameObject> _bossRocksPool;
    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody>();
    }

    /*void Start()
    {
        //Vector3 localForward = _refer.transform.TransformDirection(transform.forward);
        Vector3 newVelocity = _refer.transform.forward * _force;
        //_rb.velocity = newVelocity;
        _rb.AddForce(newVelocity * _force,ForceMode.Impulse);
    }*/
   /* private void OnEnable()
    {
        if (_refer != null)
        {
            Vector3 newVelocity = _refer.transform.forward * _force;
            //_rb.velocity = newVelocity;
            _rb.AddForce(newVelocity * _force, ForceMode.Impulse);
        }
        Debug.Log(_refer);
        /* Debug.Log(_refer+"enable");
         Vector3 localForward = _refer.transform.TransformDirection(Vector3.forward);
         Vector3 newVelocity = localForward * _force;
         //_rb.velocity = newVelocity;
         _rb.AddForce(localForward * _force);
         //_rb.velocity = new Vector3(0, 0, _force);
    }*/

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

    public void SetProjectileForce(float force, GameObject reference)
    {
        _force = force;
        _refer = reference;
    }
}
