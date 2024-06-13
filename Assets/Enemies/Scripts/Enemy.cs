using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : EnemyBase
{
    [SerializeField] private IdleSOBase _idleLogic;
    [SerializeField] private MoveSOBase _moveLogic;
    [SerializeField] private MoveSOBase _attackLogic;

    // private EnemyHealthManager _healthManager;

    private IdleSOBase IdleBaseInstance;// { get; set; }
    private MoveSOBase MoveBaseInstance;// { get; set; }
    private MoveSOBase AttackBaseInstance;

    private void Awake()
    {
        _healthManager = GetComponent<EnemyHealthManager>();
        if (_idleLogic != null) { IdleBaseInstance = Instantiate(_idleLogic); }
        if (_moveLogic != null) { MoveBaseInstance = Instantiate(_moveLogic); }
        if (_attackLogic != null) { AttackBaseInstance = Instantiate(_attackLogic); }
        //AttackBaseInstance = Instantiate(_attackLogic);
    }
    void Start()
    {
        IdleBaseInstance.Initialize(gameObject,this);
        MoveBaseInstance.Initialize(gameObject, this);
       // AttackBaseInstance.Initialize(gameObject, this);
    }

    // Update is called once per frame
    void Update()
    {       
        Move();
    }

    protected override void Idle()
    {
        if (IdleBaseInstance != null) { IdleBaseInstance.IdleLogic(); }   
    }

    protected override void Move()
    {
        if (_moveLogic != null) { MoveBaseInstance.MoveLogic(); }
        
        //transform.position = Vector3.MoveTowards(transform.position, base._moveTarget.position, base._movSpeed* Time.deltaTime);
    }
    protected override void Attack()
    {
        if (AttackBaseInstance != null) { Debug.Log(" Atacando"); }        
    }
}
