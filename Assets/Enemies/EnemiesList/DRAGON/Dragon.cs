
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : EnemyBase
{
    [Header("Behaviour SOs")]
    [SerializeField] private MoveSOBase _chasePlayerLogic;
    [SerializeField] private AttackSOBase _attackLogic;

    private MoveSOBase ChaseBaseInstance;
    private AttackSOBase AttackBaseInstance;

    [Header("Projectile data")]
    [SerializeField] private int _attackDistance;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _projectileForce;
    [SerializeField] private GameObject _projectile;

    [Header("flyheigth")]
    [SerializeField] private float flyHeight;

    private NavMeshAgent _agent;

    private void Awake()
    {        
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _healthManager = GetComponent<EnemyHealthManager>();
        _effectsManager = GetComponent<EnemyEffectsManager>();
        _lootBag = GetComponent<LootBag>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        AnimControllerInstance = Instantiate(AnimsController);
        if (_chasePlayerLogic != null) { ChaseBaseInstance = Instantiate(_chasePlayerLogic); }
        if (_attackLogic != null) { AttackBaseInstance = Instantiate(_attackLogic); }
    }

    void Start()
    {     
        ChaseBaseInstance.Initialize(gameObject, this, _agent);
        AttackBaseInstance.Initialize(gameObject, this, _agent, _attackSpeed);
        AttackBaseInstance.InitProjectileData(gameObject.GetComponentInChildren<ParticleSystem>().transform, _projectile, _projectileForce);

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
                    _effectsManager.AttackEffect();
                    AnimControllerInstance.Playanimation(_animator, AttackAnim);                  
                }
            }
            else if(AnimControllerInstance.ISAnimationEnded(_animator,AttackAnim))
            {
                _agent.isStopped = false;
                ChangeState(State.Move); 
            }

        }
        else { ChangeState(State.Die); }
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

    public void spawnProjectile()
    {
        AttackBaseInstance.AttackLogic(_animator);
    }
}
