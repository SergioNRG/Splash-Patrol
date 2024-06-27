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
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private float _force = 15f;
    [SerializeField] private float _rotationSpeed = 200f;

    private ObjectPool<GameObject> _bossRocksPool;
    private Transform _initPos;
    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _rb.AddForce(transform.forward * _force, ForceMode.Impulse);
    }
 

    void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        if (_isCounting) { _timer += Time.deltaTime; }
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("yuuup");
            //Destroy(gameObject);
            gameObject.SetActive(false);
            _bossRocksPool.Release(gameObject);
        }
    }*/

    private void OnCollisionStay(Collision collision)
    {
        _isCounting = true;
        if (_timer >= 2)
        {
           // GameObject go = Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
            _timer = 0;
            //Destroy(gameObject);
            
            gameObject.SetActive(false);
            //transform.position = _initPos.position;
            _bossRocksPool.Release(gameObject);
        }                
    }

    public void SetPool(ObjectPool<GameObject> pool)//,Transform trans)
    {
        _bossRocksPool = pool;
       // _initPos = trans;
    }
}
