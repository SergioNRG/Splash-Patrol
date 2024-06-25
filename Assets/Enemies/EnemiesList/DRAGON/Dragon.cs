using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : EnemyBase
{
    // [SerializeField] private IdleSOBase _idleLogic;
    [SerializeField] private MoveSOBase _chasePlayerLogic;
    [SerializeField] private AttackSOBase _attackLogic;

    // private IdleSOBase IdleBaseInstance;// { get; set; }
    private MoveSOBase ChaseBaseInstance;// { get; set; }
    private AttackSOBase AttackBaseInstance;// { get; set; } 

    [SerializeField] private int _attackDistance;
    [SerializeField] private float _attackSpeed;
    public GameObject Projectile;
    [SerializeField] private float flyHeight;

    private NavMeshAgent _agent;

    private void OnEnable()
    {
        ChaseAnim = AnimsController.Anims.Single(ChaseAnim => ChaseAnim.AnimKey == "CHASE").AnimName;
        AttackAnim = AnimsController.Anims.Single(AttackAnim => AttackAnim.AnimKey == "ATTACK").AnimName;
        DieAnim = AnimsController.Anims.Single(DieAnim => DieAnim.AnimKey == "DIE").AnimName;
    }

    private void Awake()
    {
        
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _healthManager = GetComponent<EnemyHealthManager>();
        _effectsManager = GetComponent<EnemyEffectsManager>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (_chasePlayerLogic != null) { ChaseBaseInstance = Instantiate(_chasePlayerLogic); }
        if (_attackLogic != null) { AttackBaseInstance = Instantiate(_attackLogic); }
    }
    // Start is called before the first frame update
    void Start()
    {
        ChaseBaseInstance.Initialize(gameObject, this, _agent);
        AttackBaseInstance.Initialize(gameObject, this, _agent, _attackSpeed);
        AttackBaseInstance.InitProjectileData(gameObject.GetComponentInChildren<ParticleSystem>().transform,Projectile);
        ChangeState(State.Move);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }

    protected override void Move()
    {

        if (_healthManager.CurrentHealth > 0)
        {
            Vector3 position = transform.position;
            position.y = _agent.nextPosition.y + flyHeight;
            transform.position = position;
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
        /* if (_healthManager.CurrentHealth > 0)
         {
             Vector3 position = transform.position;
             position.y = _agent.nextPosition.y + flyHeight;
             transform.position = position;
             if (Vector3.Distance(transform.position, _playerTransform.position) <= _attackDistance)
             {
                 ChangeState(State.Attack);
             }

             if (!AnimsController.ISAnimationPlaying(_animator, AttackAnim))
             {
                 Debug.Log(!AnimsController.ISAnimationPlaying(_animator, AttackAnim));
                 // AnimsController.Playanimation(_animator, ChaseAnim);
                 _effectsManager.ChaseEffect();
                 if (_chasePlayerLogic != null) { ChaseBaseInstance.MoveLogic(); }
             }
         }
         else { ChangeState(State.Die); }*/
    }

    protected override void Attack()
    {
        if (_healthManager.CurrentHealth > 0)
        {
            Vector3 position = transform.position;
            position.y = _agent.nextPosition.y + flyHeight;
            transform.position = position;
           
            if (Vector3.Distance(transform.position, _playerTransform.position) <= _attackDistance)
            {
                if (_attackLogic != null)
                {
                    Vector3 lookTarget = new Vector3(_playerTransform.position.x, _playerTransform.position.y + 1f, _playerTransform.position.z);
                    transform.LookAt(lookTarget);
                    // transform.LookAt(lookTarget);
                    // _animator.SetFloat("AttackSpeed", 1 / _attackSpeed);
                    _effectsManager.AttackEffect();
                    AnimsController.Playanimation(_animator, AttackAnim);                  
                }
            }
            else if(AnimsController.ISAnimationEnded(_animator,AttackAnim))
            {
                _agent.isStopped = false;
                ChangeState(State.Move); 
            }

        }
        else { ChangeState(State.Die); }
    }

    protected override void Die()
    {
        //Vector3 targetPosition = new Vector3(transform.position.x, 0, transform.position.z);
        //_agent.destination = targetPosition;
       // transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2 * Time.deltaTime);
        _agent.isStopped = true;
    }

    public void spawnProjectile()
    {
        AttackBaseInstance.AttackLogic(_animator);
    }
}
