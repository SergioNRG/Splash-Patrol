using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectsManager : MonoBehaviour
{

    [SerializeField] private EnemyHealthManager _healthManager;
    // Start is called before the first frame update
    void Start()
    {
        _healthManager = GetComponent<EnemyHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamageEffect(int damage)
    {
        if (_healthManager.CurrentHealth != 0 ) { Debug.Log("enemy taking damage"); }
        
    }

    public void Die(Vector3 position)
    {
        Debug.Log("enemy morreu");
        Destroy(gameObject);
    }
}
