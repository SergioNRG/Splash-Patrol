using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Pawn : EnemyBase
{
    //[SerializeField] private IdleSOBase _idleLogic;
    [SerializeField] private MoveSOBase _moveLogic;

    //private IdleSOBase IdleBaseInstance;// { get; set; }
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
       // if (_idleLogic != null) { IdleBaseInstance = Instantiate(_idleLogic); }
        if (_moveLogic != null) { MoveBaseInstance = Instantiate(_moveLogic); }
    }

    // Start is called before the first frame update
    void Start()
    {
        //IdleBaseInstance.Initialize(gameObject, this, gameObject.GetComponent<NavMeshAgent>());
        MoveBaseInstance.Initialize(gameObject, this, _agent);
        
    }

    void Update()
    {
        if (_healthManager.CurrentHealth <= 0)
        {
            Debug.Log("tatatat");
            AnimsController.ChangeAnimationState(_animator, MoveAnim, DieAnim);
            _agent.isStopped = true;
        }
        else
        {
            Debug.Log("oi");
            AnimsController.ChangeAnimationState(_animator, MoveAnim, MoveAnim);
            Move();
        }

    }

   /* protected override void Idle()
    {
        if (IdleBaseInstance != null) { IdleBaseInstance.IdleLogic(); }
    }*/

    protected override void Move()
    {
        if (_moveLogic != null) { MoveBaseInstance.MoveLogic(); }

        //transform.position = Vector3.MoveTowards(transform.position, base._moveTarget.position, base._movSpeed* Time.deltaTime);
    }
}
