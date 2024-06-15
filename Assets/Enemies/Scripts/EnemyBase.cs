using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //protected int _maxHealth;
    //[SerializeField] protected int _maxHealth;

    // [field:SerializeField]public int MovSpeed { get; protected set; }
    //[SerializeField] protected int _movSpeed;
    // [SerializeField] protected int _attackDamage;
    //protected int _attackSpeed;
    //[SerializeField] protected Transform _moveTarget;
    #region STRINGS FOR ANIMATIONS NAMES  

    // animations data

    [field:SerializeField] public AnimsController AnimsController { get; protected set; }
    [field: SerializeField] public Animator _animator { get; protected set; }// just to see
    [field: SerializeField] public string MoveAnim { get; protected set; }// just to see
    [field: SerializeField] public string ChaseAnim { get; protected set; }// just to see
    [field: SerializeField] public string AttackAnim { get; protected set; }// just to see
    [field: SerializeField] public string DieAnim { get; protected set; }// just to see

    #endregion

    protected EnemyHealthManager _healthManager;

    protected virtual void Idle() { }
    protected virtual void Attack() { }
    protected virtual void Move() { }

    protected virtual void Heal() { }
}
