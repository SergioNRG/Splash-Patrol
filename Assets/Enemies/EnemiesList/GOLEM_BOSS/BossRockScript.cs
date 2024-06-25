using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRockScript : MonoBehaviour
{
    private Transform _playerTransform;
    private Rigidbody _rb;
    private float _timer = 0;
    private bool _isCounting;
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private float _force = 15f;
    [SerializeField] private float _rotationSpeed = 200f;

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _rb.AddForce(transform.parent.forward * _force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        if (_isCounting) { _timer += Time.deltaTime; }
    }

    /* private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
             Destroy(gameObject);
             // Debug.Log("yuuup");
         }
         //else { Destroy(gameObject); }

     }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("yuuup");
            Destroy(gameObject);
        }
    }

   private void OnCollisionStay(Collision collision)
   {
        _isCounting = true;
        if (_timer >= 2)
        {
            GameObject go = Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
            _timer = 0;
            Destroy(gameObject);
        }                
   }

}
