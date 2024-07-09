using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _coinPoints;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(_coinPoints);
            gameObject.SetActive(false);

        }
    }

}
