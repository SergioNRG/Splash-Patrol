using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Config", menuName = "Guns/Shoot Config", order = 2)]
public class ShootConfig : ScriptableObject
{
    public LayerMask HitMask;
    public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.1f);
    public float FireRate = 0.25f;

    [Header("Recoil Values")]
    public float _recoilx;
    public float _recoily;
    public float _recoilz;

    public float _kickBackz;

    public float _snappiness, _returnAmount;
}
