using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : EnemyBase
{
    [SerializeField] private IdleSOBase _idleLogic;
    [SerializeField] private MoveSOBase _moveLogic;
    // Start is called before the first frame update
    void Start()
    {
        _idleLogic.Initialize(gameObject,this);
        _moveLogic.Initialize(gameObject, this);
    }

    // Update is called once per frame
    void Update()
    {
        _idleLogic.IdleLogic();
        Chase();
    }

    protected override void Chase()
    {
        Debug.Log("entrou");
        _moveLogic.MoveLogic();
        //transform.position = Vector3.MoveTowards(transform.position, base._moveTarget.position, base._movSpeed* Time.deltaTime);
    }
    protected override void Attack()
    {
        Debug.Log(" Atacando");
    }
}
