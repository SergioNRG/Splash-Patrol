using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MeleeWeapons", menuName = "Weapons/MeleeWeapons/MeleeAttackConfig", order = 2)]
public class MeleeAttackConfigSO : ScriptableObject
{
    [Header("Attacking Config")]
    public float attackDistance = 3f;
    public float attackSpeed = 1f;
    public int attackDamage = 25;
    public LayerMask attackLayer;


}
