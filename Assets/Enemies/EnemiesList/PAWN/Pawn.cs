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
 
    private void OnEnable()
    {
        MoveAnim = AnimsController.Anims.Single(MoveAnim => MoveAnim.AnimKey == "MOVE").AnimName;
        DieAnim = AnimsController.Anims.Single(DieAnim => DieAnim.AnimKey == "DIE").AnimName;
        MoveBaseInstance.Initialize(gameObject, this, _agent);
        ChangeState(State.Move);
        Debug.Log(MoveAnim);
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _healthManager = GetComponent<EnemyHealthManager>();
        _effectsManager = GetComponent<EnemyEffectsManager>();
        if (_moveLogic != null) { MoveBaseInstance = Instantiate(_moveLogic); }
    }


    void Start()
    {
        MoveBaseInstance.Initialize(gameObject, this, _agent);
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
                //_effectsManager.MoveEffect();
                MoveBaseInstance.MoveLogic(); 
            }
        }else { ChangeState(State.Die); }
    }

    protected override void Die()
    {
        _agent.isStopped = true;
    }

}
