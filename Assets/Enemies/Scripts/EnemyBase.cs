using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //protected int _maxHealth;
    [SerializeField] protected int _movSpeed;
    [SerializeField] protected int _attackDamage;
    //protected int _attackSpeed;
    [SerializeField] protected Transform _moveTarget;

    protected virtual void Attack() { }
    protected virtual void Chase() { }
}
