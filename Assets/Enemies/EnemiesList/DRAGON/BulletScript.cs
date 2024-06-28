using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class BulletScript : MonoBehaviour,IPooled
{
    //[SerializeField] private float _force;
    private Transform _playerTransform;
    //private Rigidbody _rb;

    private ObjectPool<GameObject> _dragonBulletPool;
    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //_rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        //_rb.AddForce(transform.parent.forward * _force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            Debug.Log("yuuup");
            gameObject.SetActive(false);
            _dragonBulletPool.Release(gameObject);
        }
        else 
        {
            gameObject.SetActive(false);
            _dragonBulletPool.Release(gameObject);
            //Destroy(gameObject);
        }
        
    }
    public void SetPool(ObjectPool<GameObject> pool)
    {
        _dragonBulletPool = pool;
    }
}
