using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapons", menuName = "Weapons/MeleeWeapons/Bat", order = 0)]
public class MeleeSO : ScriptableObject
{
    public MeleeWeaponType MelleType;
    public string Name;
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    private MonoBehaviour _activeMonoBehaviour;
    private GameObject _model;
    // verify if they are all needed
    private Vector3 _currentRotation, _targetRotation, _targetPosition, _currentPosition, _initialPosition;

    private Transform _camHolderTransform;
    public void Spawn(Transform Parent, MonoBehaviour activeMonoBehaviour)
    {
        this._activeMonoBehaviour = activeMonoBehaviour;

        _model = Instantiate(ModelPrefab);
        _model.transform.SetParent(Parent, false);
        _model.transform.localPosition = SpawnPoint;
        _model.transform.localRotation = Quaternion.Euler(SpawnRotation);


        _initialPosition = _model.transform.localPosition;

        // only works if the only camera on the scene are the player camera
        _camHolderTransform = GameObject.FindObjectOfType<Camera>().transform.parent;
    }
    public void Swipe()
    {
        Debug.Log("swiping");
    }
}
