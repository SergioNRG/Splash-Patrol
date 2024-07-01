
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class GolemBoss : EnemyBase
{
    [SerializeField] private IdleSOBase _IdleLogic;
    [SerializeField] private MoveSOBase _chasePlayerLogic;
    [SerializeField] private AttackSOBase _attackLogic;

    private IdleSOBase IdleBaseInstance;// { get; set; }
    private MoveSOBase ChaseBaseInstance;// { get; set; }
    private AttackSOBase AttackBaseInstance;// { get; set; } 

    [SerializeField] private int _attackDistance;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _projectileForce;

    private NavMeshAgent _agent;

    [SerializeField] private int repeatCount = 3; // Number of times to repeat the animation
    private Coroutine _coroutine;
    private void OnEnable()
    {
        IdleAnim = AnimsController.Anims.Single(IdleAnim => IdleAnim.AnimKey == "IDLE").AnimName;
        RoarAnim = AnimsController.Anims.Single(RoarAnim => RoarAnim.AnimKey == "ROAR").AnimName;
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
        if (_IdleLogic != null) { IdleBaseInstance = Instantiate(_IdleLogic); }
        if (_chasePlayerLogic != null) { ChaseBaseInstance = Instantiate(_chasePlayerLogic); }
        if (_attackLogic != null) { AttackBaseInstance = Instantiate(_attackLogic); }
    }

    void Start()
    {
        ChaseBaseInstance.Initialize(gameObject, this, _agent);
        AttackBaseInstance.Initialize(gameObject, this, _agent, _attackSpeed);
        AttackBaseInstance.InitProjectileData(gameObject.GetComponentInChildren<ParticleSystem>().transform, Projectile,_projectileForce);
        AnimsController.ResetCurrentRepeat();
        ChangeState(State.Idle);
        _coroutine = StartCoroutine(AnimsController.RepeatAnimation(repeatCount,_animator,IdleAnim));
    }


    void Update()
    {        
        StateMachine();      
    }


    protected override void Idle()
    {        
        if (_healthManager.CurrentHealth > 0)
        {          
            if (AnimsController.GetCurrentRepeat() >= repeatCount)
            {
                
                if (AnimsController.ISAnimationEnded(_animator, IdleAnim))
                {
                    AnimsController.ResetCurrentRepeat();
                    if (_coroutine != null)
                    {
                        StopCoroutine(_coroutine);
                    }
                   
                    ChangeState(State.Roar);
                }
                
            }
        }
        else { ChangeState(State.Die); }
    }

 

    protected override void Roar()
    {
        if (_healthManager.CurrentHealth > 0)
        {
            _effectsManager.RoarEffect();

            if (AnimsController.ISAnimationEnded(_animator, RoarAnim) == true)
            {
                ChangeState(State.Attack);
            }
        }
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
                // AnimsController.Playanimation(_animator, ChaseAnim);
                // if (_chasePlayerLogic != null) { ChaseBaseInstance.MoveLogic(); }
            }

            /*_effectsManager.ChaseEffect();
            Debug.Log(_chasePlayerLogic);
            if (_chasePlayerLogic != null) { ChaseBaseInstance.MoveLogic(); }*/


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
                    // AnimsController.Playanimation(_animator, AttackAnim);
                }
            }
            else if (AnimsController.ISAnimationEnded(_animator, AttackAnim))
            {
                _agent.isStopped = false;
                ChangeState(State.Move);
            }
            /* if (_attackLogic != null)
             {
                 Vector3 lookTarget = new Vector3(_playerTransform.position.x, _playerTransform.position.y + 1f, _playerTransform.position.z);
                 transform.LookAt(lookTarget);
                 _agent.isStopped = true;
                 _effectsManager.AttackEffect();
                 if (AnimsController.ISAnimationEnded(_animator, AttackAnim) == true)
                 {
                     Debug.Log("oi");
                     _agent.isStopped = false;
                     ChangeState(State.Move);
                 }

             }*/
        }
        else { ChangeState(State.Die); }
    }

    public void spawnProjectile()
    {
        AttackBaseInstance.AttackLogic(_animator);
    }

    protected override void Die()
    {
        //Vector3 targetPosition = new Vector3(transform.position.x, 0, transform.position.z);
        //_agent.destination = targetPosition;
        // transform.position = Vector3.MoveTowards(transform.position, targetPosition, 2 * Time.deltaTime);
        _agent.isStopped = true;
    }

   
}
