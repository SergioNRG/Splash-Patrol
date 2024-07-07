using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _coinPoints;
   // private ObjectPool<GameObject> _coinPool;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(_coinPoints);
            gameObject.SetActive(false);
            LootBag.LootPool.Release(gameObject);
        }
    }

   /* public void SetPool(ObjectPool<GameObject> pool)
    {
        _coinPool = pool;
    }*/
}
