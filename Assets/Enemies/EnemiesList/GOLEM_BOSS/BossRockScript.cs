
using UnityEngine;
using UnityEngine.Pool;


public class BossRockScript : MonoBehaviour,IPooled
{
    [SerializeField] private GameObject _enemyToSpawn;// LATER make the rock spawn a golem 
    [SerializeField] private float _rotationSpeed = 200f;
    [SerializeField] private int _bossRockDamage;

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
            if (_bossRockDamage != 0)
            {
                collision.collider.GetComponent<IDamageable>().ApplyDamage(_bossRockDamage);
            }
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
