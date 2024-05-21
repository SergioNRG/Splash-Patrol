using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    public GunsSO GunSO;
    public Vector3 spinDirection = Vector3.up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinDirection);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerGunSelector gunSelector))
        {
            gunSelector.PickupGun(GunSO);
            Destroy(gameObject);
        }
    }
}
