using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPShield : MonoBehaviour
{
    [SerializeField] private int _hpBonus;
    [SerializeField] private AudioClip _hpUpSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IHealable>().ApplyHeal(_hpBonus);
            SoundFXManager.instance.PlayFXSound(_hpUpSound, 0.1f);
            gameObject.SetActive(false);

            Debug.Log(other.GetComponent<PlayerHealthManager>().CurrentHealth);
        }
    }

}
