
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


public class GolemBoss : EnemyBase
{
    [SerializeField] private IdleSOBase _IdleLogic;
    [SerializeField] private MoveSOBase _chasePlayerLogic;
    [SerializeField] private AttackSOBase _attackLogic;

    private IdleSOBase IdleBaseInstance;
    private MoveSOBase ChaseBaseInstance;
    private AttackSOBase AttackBaseInstance;



    [SerializeField] private int _attackDistance;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _projectileForce;


    [SerializeField] private AudioClip _fxSound;


    private NavMeshAgent _agent;

    [SerializeField] private int repeatCount ; // Number of times to repeat the animation
    private Coroutine _coroutine;


    private void OnEnable()
    {
        
        ChangeState(State.Idle);
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _healthManager = GetComponent<EnemyHealthManager>();
        _effectsManager = GetComponent<EnemyEffectsManager>();
        _lootBag = GetComponent<LootBag>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        AnimControllerInstance = Instantiate(AnimsController);
        if (_IdleLogic != null) { IdleBaseInstance = Instantiate(_IdleLogic); }
        if (_chasePlayerLogic != null) { ChaseBaseInstance = Instantiate(_chasePlayerLogic); }
        if (_attackLogic != null) { AttackBaseInstance = Instantiate(_attackLogic); }
    }

    void Start()
    {
        ChaseBaseInstance.Initialize(gameObject, this, _agent);
        AttackBaseInstance.Initialize(gameObject, this, _agent, _attackSpeed);
        AttackBaseInstance.InitProjectileData(gameObject.GetComponentInChildren<ParticleSystem>().transform, Projectile,_projectileForce);

        IdleAnim = AnimControllerInstance.Anims.Single(IdleAnim => IdleAnim.AnimKey == "IDLE").AnimName;
        RoarAnim = AnimControllerInstance.Anims.Single(RoarAnim => RoarAnim.AnimKey == "ROAR").AnimName;
        ChaseAnim = AnimControllerInstance.Anims.Single(ChaseAnim => ChaseAnim.AnimKey == "CHASE").AnimName;
        AttackAnim = AnimControllerInstance.Anims.Single(AttackAnim => AttackAnim.AnimKey == "ATTACK").AnimName;
        DieAnim = AnimControllerInstance.Anims.Single(DieAnim => DieAnim.AnimKey == "DIE").AnimName;

        AnimControllerInstance.ResetCurrentRepeat();
        
        _coroutine = StartCoroutine(AnimControllerInstance.RepeatAnimation(repeatCount,_animator,IdleAnim));
    }


    void Update()
    {        
        StateMachine();      
    }


    protected override void Idle()
    {        
        if (_healthManager.CurrentHealth > 0)
        {
            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(AnimControllerInstance.RepeatAnimation(repeatCount, _animator, IdleAnim));
            }
            _effectsManager.Idleffect();
            if (AnimControllerInstance.GetCurrentRepeat() >= repeatCount && _healthManager.CurrentHealth > 0)
            {
                Debug.Log("tou repet");
                if (AnimControllerInstance.ISAnimationEnded(_animator, IdleAnim ) && _healthManager.CurrentHealth > 0)
                {
                    Debug.Log("anim ended");
                    AnimControllerInstance.ResetCurrentRepeat();
                    
                    if (_coroutine != null)
                    {
                        StopCoroutine(_coroutine);
                        _coroutine = null;
                    }
                    if (_fxSound != null)
                    {
                        SoundFXManager.instance.PlayFXSound(_fxSound, 0.08f);
                    }
                    ChangeState(State.Roar);
                }
                else { ChangeState(State.Die); }

            }
            //else { ChangeState(State.Die); }
        }
        else { ChangeState(State.Die); }
    }

    protected override void Roar()
    {
        if (_healthManager.CurrentHealth > 0)
        {
            _effectsManager.RoarEffect();
            Debug.Log("tou roar");

            if (AnimControllerInstance.ISAnimationEnded(_animator, RoarAnim))
            {
                Debug.Log("tou roar ended");
                if (Vector3.Distance(transform.position, _playerTransform.position) <= _attackDistance)
                {
                    ChangeState(State.Attack);
                }else                  
                {
                   /* if (_coroutine == null)
                    {
                        _coroutine = StartCoroutine(AnimControllerInstance.RepeatAnimation(repeatCount, _animator, IdleAnim));
                    }*/

                    ChangeState(State.Idle);
                }
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
                    //Vector3 lookTarget = new Vector3(_playerTransform.position.x, _playerTransform.position.y + 1f, _playerTransform.position.z);
                    Vector3 lookTarget = new Vector3(_playerTransform.position.x, 0, _playerTransform.position.z);
                    transform.LookAt(lookTarget);
                    _agent.isStopped = true;
                    _effectsManager.AttackEffect();
                }
            }
            else if (AnimControllerInstance.ISAnimationEnded(_animator, AttackAnim))
            {
                //_agent.isStopped = false;
                ChangeState(State.Idle);
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
        if (AnimControllerInstance.ISAnimationEnded(_animator, DieAnim))
        {
        
            _lootBag.SpawnLoot(transform);

            ScoreManager.Instance.AddScore(PointsToGive);

            _healthManager.CurrentHealth = _healthManager.MaxHealth;
            ReturnToPool();
        }
        
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
