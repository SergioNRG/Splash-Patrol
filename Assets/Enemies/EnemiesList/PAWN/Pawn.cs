
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Pawn : EnemyBase
{
    [Header("Behaviour SO")]
    [SerializeField] private MoveSOBase _moveLogic;

    private MoveSOBase MoveBaseInstance;
    private NavMeshAgent _agent;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _healthManager = GetComponent<EnemyHealthManager>();
        _effectsManager = GetComponent<EnemyEffectsManager>();
        _lootBag = GetComponent<LootBag>();


        AnimControllerInstance = Instantiate(AnimsController);
        if (_moveLogic != null) { MoveBaseInstance = Instantiate(_moveLogic); }
    }


    void Start()
    {
        MoveBaseInstance.Initialize(gameObject, this, _agent);       
        MoveAnim = AnimControllerInstance.Anims.Single(MoveAnim => MoveAnim.AnimKey == "MOVE").AnimName;
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
            _agent.isStopped = false;
            if (_moveLogic != null) 
            {
                MoveBaseInstance.MoveLogic(); 
            }
        }else { ChangeState(State.Die); }
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
