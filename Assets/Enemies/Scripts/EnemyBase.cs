using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyEffectsManager))]
[RequireComponent(typeof(EnemyHealthManager))]

public class EnemyBase : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Attack,
        Die,
        Roar
    }

    protected State _currentState;

    [field: SerializeField] public int PointsToGive { get; protected set; }

    [SerializeField] protected int damage;
    [SerializeField] protected Transform _playerTransform;
    
    protected EnemyHealthManager _healthManager;
    protected EnemyEffectsManager _effectsManager;
    protected LootBag _lootBag;

    #region STRINGS FOR ANIMATIONS NAMES  

    // animations data

    [field:SerializeField] public AnimsController AnimsController { get; protected set; }
    [field: SerializeField] public Animator _animator { get; protected set; }// just to see
    [field: SerializeField] public string IdleAnim { get; protected set; }// just to see
    [field: SerializeField] public string RoarAnim { get; protected set; }// just to see
    [field: SerializeField] public string MoveAnim { get; protected set; }// just to see
    [field: SerializeField] public string ChaseAnim { get; protected set; }// just to see
    [field: SerializeField] public string AttackAnim { get; protected set; }// just to see
    [field: SerializeField] public string DieAnim { get; protected set; }// just to see

    #endregion
    public AnimsController AnimControllerInstance { get;protected set; }
    
    protected virtual void Idle() { }
    protected virtual void Attack() { }
    protected virtual void Move() { }

    protected virtual void Die() { }

    protected virtual void Roar() { }

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

            case State.Roar:
                Roar();
                break;


        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (damage != 0)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<IDamageable>().ApplyDamage(damage);
            }
        }      
    }
}
