using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField] private GunType _gunType;
    [SerializeField] private Transform _gunParent;
    [SerializeField] private List<GunsSO> _guns;


    [Header("Runtime Filled")]
    public GunsSO ActiveGun;

    private void Start()
    {
        GunsSO gun = _guns.Find(gun => gun.Type == _gunType);

        if (gun == null) 
        {
            Debug.Log("error ... no GunSO found");
            return;
        }

        ActiveGun = gun;
        gun.Spawn(_gunParent,this);
    }
}
