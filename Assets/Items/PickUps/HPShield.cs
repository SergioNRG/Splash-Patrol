using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPShield : MonoBehaviour
{
    [SerializeField] private int _hpBonus;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IHealable>().ApplyHeal(_hpBonus);
            gameObject.SetActive(false);

            Debug.Log(other.GetComponent<PlayerHealthManager>().CurrentHealth);
        }
    }

}
