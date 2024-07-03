using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Pawn : EnemyBase
{
    [SerializeField] private MoveSOBase _moveLogic;

    private MoveSOBase MoveBaseInstance;// { get; set; }
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _healthManager = GetComponent<EnemyHealthManager>();
        _effectsManager = GetComponent<EnemyEffectsManager>();

        AnimControllerInstance = Instantiate(AnimsController);
        if (_moveLogic != null) { MoveBaseInstance = Instantiate(_moveLogic); }
    }


    void Start()
    {
        MoveBaseInstance.Initialize(gameObject, this, _agent);       
        MoveAnim = AnimControllerInstance.Anims.Single(MoveAnim => MoveAnim.AnimKey == "MOVE").AnimName;
        DieAnim = AnimControllerInstance.Anims.Single(DieAnim => DieAnim.AnimKey == "DIE").AnimName;
    }
    private void OnEnable()
    {       
        ChangeState(State.Move);
    }

    void Update()
    {
        StateMachine();
    }

    protected override void Move()
    {
        if (_healthManager.CurrentHealth > 0)
        {
            _agent.isStopped = false;
            if (_moveLogic != null) 
            {
                MoveBaseInstance.MoveLogic(); 
            }
        }else { ChangeState(State.Die); }
    }

    protected override void Die()
    {
        _agent.isStopped = true;
        if (AnimControllerInstance.ISAnimationEnded(_animator, DieAnim))
        {

            EnemySpawner.instance.numbenemies--;
            _healthManager.CurrentHealth = _healthManager.MaxHealth;
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

}
