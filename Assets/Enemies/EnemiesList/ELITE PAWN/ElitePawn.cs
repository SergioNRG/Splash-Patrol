using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ElitePawn : EnemyBase
{

    [SerializeField] private MoveSOBase _moveLogic;
    [SerializeField] private MoveSOBase _chasePlayerLogic;
    [SerializeField] private AttackSOBase _attackLogic;


    private MoveSOBase MoveBaseInstance;// { get; set; }
    private MoveSOBase ChaseBaseInstance;// { get; set; }
    private AttackSOBase AttackBaseInstance;// { get; set; } 

    [SerializeField] private int _attackDistance;
    [SerializeField] private float _attackSpeed;

    private NavMeshAgent _agent;


    private void Awake()
    {
        _agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponentInParent<Animator>();
        _healthManager = GetComponent<EnemyHealthManager>();
        _effectsManager = GetComponent<EnemyEffectsManager>();
        _lootBag = GetComponent<LootBag>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;


        AnimControllerInstance = Instantiate(AnimsController);
        if (_moveLogic != null) { MoveBaseInstance = Instantiate(_moveLogic); }
        if (_chasePlayerLogic != null) { ChaseBaseInstance = Instantiate(_chasePlayerLogic); }
        if (_attackLogic != null) { AttackBaseInstance = Instantiate(_attackLogic); }
    }
    void Start()
    {
        MoveBaseInstance.Initialize(gameObject, this, _agent);
        ChaseBaseInstance.Initialize(gameObject, this,_agent);
        AttackBaseInstance.Initialize(gameObject, this, _agent, _attackSpeed);
        
        MoveAnim = AnimControllerInstance.Anims.Single(MoveAnim => MoveAnim.AnimKey == "MOVE").AnimName;
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
            else if (_healthManager.CurrentHealth < _healthManager.MaxHealth)
            {
                _effectsManager.ChaseEffect();
                ChaseBaseInstance.MoveLogic();
            }
            else
            {
                _effectsManager.MoveEffect();
                MoveBaseInstance.MoveLogic();
            }
        }else { ChangeState(State.Die); }
    }

    protected override void Attack()
    {
        if (_healthManager.CurrentHealth > 0)
        {

            if (Vector3.Distance(transform.position, _playerTransform.position) <= _attackDistance)
            {               
                if (_attackLogic != null) 
                {
                    _effectsManager.AttackEffect();
                    AttackBaseInstance.AttackLogic(_animator); 
                }
            }else
            {
                if (AnimControllerInstance.ISAnimationEnded(_animator, AttackAnim))
                {
                    ChangeState(State.Move);
                }
                
            }
               
        }else { ChangeState(State.Die); }
    }


    protected override void Die()
    {
        _agent.isStopped = true;
        if (AnimControllerInstance.ISAnimationEnded(_animator, DieAnim))
        {

            var loot = _lootBag.GetLoot(transform.parent.position);
            /*if (loot != null)
            {
                loot.transform.position = transform.parent.position;
            }*/


            //loot.SetActive(true);
            ScoreManager.Instance.AddScore(PointsToGive);
            //EnemySpawner.instance.numbenemies--;
            _healthManager.CurrentHealth = _healthManager.MaxHealth;
            ReturnToPool();
        }      
    }

    public void ReturnToPool()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
