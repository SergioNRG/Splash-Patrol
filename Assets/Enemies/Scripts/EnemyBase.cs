using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //protected int _maxHealth;
   //[SerializeField] protected int _maxHealth;

    [field:SerializeField]public int MovSpeed { get; protected set; }
    //[SerializeField] protected int _movSpeed;
    [SerializeField] protected int _attackDamage;
    //protected int _attackSpeed;
    //[SerializeField] protected Transform _moveTarget;

    protected EnemyHealthManager _healthManager;

    protected virtual void Idle() { }
    protected virtual void Attack() { }
    protected virtual void Move() { }

    protected virtual void Heal() { }
}
