using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPShield : MonoBehaviour
{
    [SerializeField] private int _hpBonus;
    // Start is called before the first frame update
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
            other.GetComponent<IHealable>().ApplyHeal(_hpBonus);
            Debug.Log(other.GetComponent<PlayerHealthManager>().CurrentHealth);
        }
    }
}
