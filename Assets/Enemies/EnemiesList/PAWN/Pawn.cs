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
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _healthManager = GetComponent<EnemyHealthManager>();
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
            if (_moveLogic != null) { MoveBaseInstance.MoveLogic(); }
        }else { ChangeState(State.Die); }
        

        //transform.position = Vector3.MoveTowards(transform.position, base._moveTarget.position, base._movSpeed* Time.deltaTime);
    }

    protected override void Die()
    {
        _agent.isStopped = true;
    }

}
