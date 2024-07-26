using UnityEngine;


public class Coin : MonoBehaviour
{
    [SerializeField] private int _coinPoints;
    [SerializeField] private AudioClip _coinsound;

    private Vector3 _spinDirection = Vector3.up;

    private void Update()
    {
        transform.Rotate(_spinDirection);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(_coinPoints);
            }
            SoundManager.instance.PlayFXSound(_coinsound, 1f);
            gameObject.SetActive(false);

        }
    }

}
