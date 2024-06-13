using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : EnemyBase
{
    [SerializeField] private IdleSOBase _idleLogic;
    [SerializeField] private MoveSOBase _moveLogic;
    // Start is called before the first frame update

    private IdleSOBase IdleBaseInstance;// { get; set; }
    private MoveSOBase MoveBaseInstance;// { get; set; }
    private void Awake()
    {
       IdleBaseInstance = Instantiate(_idleLogic);
       MoveBaseInstance = Instantiate(_moveLogic);
    }
    void Start()
    {
        IdleBaseInstance.Initialize(gameObject,this);
        MoveBaseInstance.Initialize(gameObject, this);
    }

    // Update is called once per frame
    void Update()
    {
       
        Move();
    }

    protected override void Idle()
    {
        IdleBaseInstance.IdleLogic();
    }

    protected override void Move()
    {
        MoveBaseInstance.MoveLogic();
        //transform.position = Vector3.MoveTowards(transform.position, base._moveTarget.position, base._movSpeed* Time.deltaTime);
    }
    protected override void Attack()
    {
        Debug.Log(" Atacando");
    }
}
