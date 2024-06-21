using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyEffectsManager))]
[RequireComponent(typeof(EnemyHealthManager))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBase : MonoBehaviour
{
    //protected int _maxHealth;
    //[SerializeField] protected int _maxHealth;

    // [field:SerializeField]public int MovSpeed { get; protected set; }
    //[SerializeField] protected int _movSpeed;
    // [SerializeField] protected int _attackDamage;
    //protected int _attackSpeed;
    //[SerializeField] protected Transform _moveTarget;
    public enum State
    {
        Idle,
        Move,
        Attack,
        Die
    }

    protected State _currentState;

    [SerializeField] protected Transform _playerTransform;

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

    protected virtual void Die() { }

    protected virtual void Heal() { }

    public void ChangeState(State newState)
    {
        if (_currentState != newState)
        {
            _currentState = newState;
        }

    }

    public void StateMachine()
    {
        switch (_currentState)
        {
            default:
            case State.Idle:
                Idle();
                break;

            case State.Move:
                Move();
                break;

            case State.Attack:
                Attack();
                break;

            case State.Die:
                Die();
                break;


        }
    }
}
