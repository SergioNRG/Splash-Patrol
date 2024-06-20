using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackSOBase : ScriptableObject
{
    protected EnemyBase enemy;
    protected NavMeshAgent enemyAgent;
    protected Transform transform;
    protected GameObject enemyObject;
    protected Transform bulletPoint;
    protected float attackSpeed;

    public Transform playerTranform;

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy, NavMeshAgent enemyAgent,float attackSpeed)
    {
        this.enemy = enemy;
        this.enemyAgent = enemyAgent;
        this.enemyObject = gameObject;
        this.transform = gameObject.transform;
        this.attackSpeed = attackSpeed;
        playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void AttackLogic( Animator animator, string animation) { }

    public virtual void InitializePoint(Transform point)
    {
        this.bulletPoint = point;
    }
}
