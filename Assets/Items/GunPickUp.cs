
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    public GunsSO GunSO;
    private Vector3 _spinDirection = Vector3.up;

    void Update()
    {
        transform.Rotate(_spinDirection);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerGunSelector gunSelector))
        {
            gunSelector.PickupGun(GunSO);
            Transform parentTrans = transform.parent;
            GameObject parent = parentTrans.gameObject;
            parent.SetActive(false);
        }
    }
}
