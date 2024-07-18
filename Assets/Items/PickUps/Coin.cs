using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _coinPoints;
    [SerializeField] private AudioClip _coinsound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(_coinPoints);
            }
            SoundFXManager.instance.PlayFXSound(_coinsound, 1f);
            gameObject.SetActive(false);

        }
    }

}
