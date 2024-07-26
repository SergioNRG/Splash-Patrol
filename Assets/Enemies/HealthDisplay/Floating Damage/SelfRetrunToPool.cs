
using UnityEngine.Pool;
using UnityEngine;

public class SelfReturnToPool : MonoBehaviour
{

    [SerializeField] private float _displayTime = 0.5f;

    private ObjectPool<GameObject> _pool;

    private void Update()
    {
        _displayTime -= Time.deltaTime;
        if ( _displayTime <= 0)
        {
            _displayTime = 0.5f;
            gameObject.SetActive(false);
            _pool.Release(gameObject);
        }
        
    }

    public void SetPool(ObjectPool<GameObject> pool)
    {
        _pool = pool;
    }
}
