using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapons", menuName = "Weapons/MeleeWeapons/Bat", order = 0)]
public class MeleeSO : WeaponSOBase
{
    public MeleeWeaponType MelleType;
    public string Name;
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    private MonoBehaviour _activeMonoBehaviour;
    public GameObject Model;
    // verify if they are all needed
    private Vector3 _currentRotation, _targetRotation, _targetPosition, _currentPosition, _initialPosition;

    private Transform _camHolderTransform;
    public override void Spawn(Transform Parent, MonoBehaviour activeMonoBehaviour)
    {
        this._activeMonoBehaviour = activeMonoBehaviour;

        Model = Instantiate(ModelPrefab);
        Model.transform.SetParent(Parent, false);
        Model.transform.localPosition = SpawnPoint;
        Model.transform.localRotation = Quaternion.Euler(SpawnRotation);


        _initialPosition = Model.transform.localPosition;

        // only works if the only camera on the scene are the player camera
        _camHolderTransform = GameObject.FindObjectOfType<Camera>().transform.parent;
    }
    public override void Attack()
    {
        Debug.Log("swiping");
    }
}
