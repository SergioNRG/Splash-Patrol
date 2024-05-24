using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField] private GunType _gunType;
    [SerializeField] private MeleeWeaponType _meleeType;
    [SerializeField] private Transform _gunParent;

    [Header("Guns SO")]
    [SerializeField] private List<GunsSO> _guns;

    [Header("Melees SO")]
    [SerializeField] private List<MeleeSO> _melees;

    [Header("Runtime Filled")]
    public GunsSO ActiveGun;
    public MeleeSO ActiveMelee;

    public List<WeaponSOBase> _activeWeapons;

    private void Start()
    {
        //gun Spawn

        GunsSO gun = _guns.Find(gun => gun.Type == _gunType);

        if (gun == null)
        {
            Debug.Log("error ... no GunSO found");
            return;
        }

        SetupGun(gun);

         ActiveGun.Model.SetActive(false);

        // melle Spawning

        MeleeSO melee = _melees.Find(melee => melee.MelleType == _meleeType);

        if (melee == null)
        {
            Debug.Log("error ... no GunSO found");
            return;
        }

        ActiveMelee = melee;
        ActiveMelee.Spawn(_gunParent, this);
        _activeWeapons.Add(ActiveMelee);

    }



    #region SETUP/DESPAWN/PICKUP
    private void SetupGun(GunsSO GunSO)
    {
        ActiveGun = GunSO.Clone() as GunsSO;
        ActiveGun.Spawn(_gunParent, this);
         _activeWeapons.Add(ActiveGun);
    }

    public void DespawnActiveGun()
    {
        _activeWeapons.Remove(ActiveGun);
        ActiveGun.Despawn();
        Destroy(ActiveGun);
    }

    

    public void PickupGun(GunsSO GunSO)
    {
        ActiveMelee.Model.SetActive(false);
        DespawnActiveGun();
        SetupGun(GunSO);
    }
    #endregion
}

