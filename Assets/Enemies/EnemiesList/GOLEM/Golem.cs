using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Golem : EnemyBase
{
    // [SerializeField] private IdleSOBase _idleLogic;
    [SerializeField] private MoveSOBase _chasePlayerLogic;
    [SerializeField] private AttackSOBase _attackLogic;

    // private IdleSOBase IdleBaseInstance;// { get; set; }
    private MoveSOBase ChaseBaseInstance;// { get; set; }
    private AttackSOBase AttackBaseInstance;// { get; set; } 

    [SerializeField] private int _attackDistance;
    public GameObject Projectile;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _projectileForce;
    // [SerializeField] private float flyHeight;

    private NavMeshAgent _agent;



    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _healthManager = GetComponent<EnemyHealthManager>();
        _effectsManager = GetComponent<EnemyEffectsManager>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        AnimControllerInstance = Instantiate(AnimsController);
        if (_chasePlayerLogic != null) { ChaseBaseInstance = Instantiate(_chasePlayerLogic); }
        if (_attackLogic != null) { AttackBaseInstance = Instantiate(_attackLogic); }
    }

    void Start()
    {
        ChaseBaseInstance.Initialize(gameObject, this, _agent);
        AttackBaseInstance.Initialize(gameObject, this, _agent, _attackSpeed);
        AttackBaseInstance.InitProjectileData(gameObject.GetComponentInChildren<ParticleSystem>().transform, Projectile, _projectileForce);

        ChaseAnim = AnimControllerInstance.Anims.Single(ChaseAnim => ChaseAnim.AnimKey == "CHASE").AnimName;
        AttackAnim = AnimControllerInstance.Anims.Single(AttackAnim => AttackAnim.AnimKey == "ATTACK").AnimName;
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

            if (Vector3.Distance(transform.position, _playerTransform.position) <= _attackDistance)
            {
                ChangeState(State.Attack);
            }
            else if (_chasePlayerLogic != null)
            {
                _effectsManager.ChaseEffect();
                ChaseBaseInstance.MoveLogic();
            }
        }
        else { ChangeState(State.Die); }
    }

    protected override void Attack()
    {
        if (_healthManager.CurrentHealth > 0)
        {
            if (Vector3.Distance(transform.position, _playerTransform.position) <= _attackDistance)
            {
                if (_attackLogic != null)
                {
                    Vector3 lookTarget = new Vector3(_playerTransform.position.x, _playerTransform.position.y + 1f, _playerTransform.position.z);
                     transform.LookAt(lookTarget);
                    _agent.isStopped = true;
                    _effectsManager.AttackEffect();
                }
            }
            else if (AnimControllerInstance.ISAnimationEnded(_animator, AttackAnim))
            {
                _agent.isStopped = false;
                ChangeState(State.Move);
            }
        }
        else { ChangeState(State.Die); }
    }
    public void spawnProjectile()
    {
        AttackBaseInstance.AttackLogic(_animator);
    }

    protected override void Die()
    {
        _agent.isStopped = true;
    }

 
}
