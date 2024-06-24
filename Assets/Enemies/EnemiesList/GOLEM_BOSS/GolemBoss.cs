using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;
using static AnimsList;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GolemBoss : EnemyBase
{
    [SerializeField] private IdleSOBase _IdleLogic;
    [SerializeField] private MoveSOBase _chasePlayerLogic;
    [SerializeField] private AttackSOBase _attackLogic;

    private IdleSOBase IdleBaseInstance;// { get; set; }
    private MoveSOBase ChaseBaseInstance;// { get; set; }
    private AttackSOBase AttackBaseInstance;// { get; set; } 

    [SerializeField] private int _attackDistance;
    public GameObject Projectile;
    [SerializeField] private float _attackSpeed;
    // [SerializeField] private float flyHeight;

    private NavMeshAgent _agent;

    public int repeatCount = 3; // Number of times to repeat the animation
    //private int currentRepeat = 0;
    float timeElapsed = 0f;
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
    // Start is called before the first frame update
    void Start()
    {
        ChaseBaseInstance.Initialize(gameObject, this, _agent);
        AttackBaseInstance.Initialize(gameObject, this, _agent, _attackSpeed);
        AttackBaseInstance.InitProjectileData(gameObject.GetComponentInChildren<ParticleSystem>().transform, Projectile);
        AnimsController.ResetCurrentRepeat();
        ChangeState(State.Idle);
        StartCoroutine(AnimsController.RepeatAnimation(repeatCount,_animator,IdleAnim));
    }

    // Update is called once per frame
    void Update()
    {
        
        StateMachine();
       
    }


    protected override void Idle()
    {
        
        if (_healthManager.CurrentHealth > 0)
        {
            if (AnimsController.GetCurrentRepeat() == repeatCount)
            {

                ChangeState(State.Roar);
            }

           // StartCoroutine(RepeatAnimation());
            //ChangeState(State.Roar);
        }
        else { ChangeState(State.Die); }
    }

 

    protected override void Roar()
    {
        _effectsManager.RoarEffect();
        //Debug.Log(!AnimsController.ISAnimationPlaying(_animator, RoarAnim));

        if (AnimsController.ISAnimationEnded(_animator, RoarAnim) == true)
        {
            Debug.Log("io");
        }
        // Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime< 1.0f);//_animator.GetCurrentAnimatorStateInfo(0).IsName(RoarAnim));


    }
    protected override void Move()
    {
        if (_healthManager.CurrentHealth > 0)
        {

           /* if (Vector3.Distance(transform.position, _playerTransform.position) <= _attackDistance)
            {
                ChangeState(State.Attack);
            }
            else
            {
                //AnimsController.Playanimation(_animator, ChaseAnim);
                _effectsManager.ChaseEffect();
                if (_chasePlayerLogic != null) { ChaseBaseInstance.MoveLogic(); }
            }

            /*if (!AnimsController.ISAnimationPlaying(_animator, AttackAnim))
            {
                AnimsController.Playanimation(_animator, ChaseAnim);
                if (_chasePlayerLogic != null) { ChaseBaseInstance.MoveLogic(); }
            }*/
        }
        else { ChangeState(State.Die); }
    }

    protected override void Attack()
    {
        if (_healthManager.CurrentHealth > 0)
        {
            if (_attackLogic != null)
            {
                Vector3 lookTarget = new Vector3(_playerTransform.position.x, _playerTransform.position.y + 1f, _playerTransform.position.z);
                transform.LookAt(lookTarget);
                _agent.isStopped = true;
                _effectsManager.AttackEffect();
               /* if (!AnimsController.ISAnimationPlaying(_animator, AttackAnim))
                {
                    _agent.isStopped = false;
                    ChangeState(State.Move);
                    Debug.Log("oi finish attack");

                }*/
                // AnimsController.Playanimation(_animator, AttackAnim);
            }
            /*
            if (Vector3.Distance(transform.position, _playerTransform.position) <= _attackDistance)
            {
                if (_attackLogic != null)
                {
                    Vector3 lookTarget = new Vector3(_playerTransform.position.x, _playerTransform.position.y + 1f, _playerTransform.position.z);
                    transform.LookAt(lookTarget);
                    _agent.isStopped = true;
                    _effectsManager.AttackEffect();
                    if (!AnimsController.ISAnimationPlaying(_animator, AttackAnim))
                    {
                        _agent.isStopped = false;
                        ChangeState(State.Move);
                        Debug.Log("oi finish attack");
                    }
                   // AnimsController.Playanimation(_animator, AttackAnim);
                }
            }
          /*  else
            {
                if (!AnimsController.ISAnimationPlaying(_animator, AttackAnim))
                {
                    _agent.isStopped = false;
                    ChangeState(State.Move);
                }

            }*/

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
