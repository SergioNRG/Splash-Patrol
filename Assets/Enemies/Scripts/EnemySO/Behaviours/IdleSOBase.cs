using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class IdleSOBase : ScriptableObject
{
    protected EnemyBase enemy;
    protected NavMeshAgent enemyAgent;
    protected Transform transform;
    protected GameObject enemyObject;

    //protected Transform playerTranform;

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy, NavMeshAgent enemyAgent)
    {
        this.enemy = enemy;
        this.enemyAgent = enemyAgent;
        this.enemyObject = gameObject;
        this.transform = gameObject.transform;

       // playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void IdleLogic() { }
}
