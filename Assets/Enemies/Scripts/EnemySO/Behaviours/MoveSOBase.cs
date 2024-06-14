using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveSOBase : ScriptableObject
{
    protected EnemyBase enemy;
    protected NavMeshAgent enemyAgent;
    protected Transform transform;
    protected GameObject enemyObject;
    
    protected Transform playerTranform;
    protected Transform portalTranform;

    public virtual void Initialize(GameObject gameObject, EnemyBase enemy,NavMeshAgent enemyAgent)
    {
        this.enemy = enemy;
        this.enemyAgent = enemyAgent;
        this.enemyObject = gameObject;
        this.transform = gameObject.transform;

        playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
        portalTranform = GameObject.FindGameObjectWithTag("Portal").transform;
    }

    public virtual void MoveLogic() { }
}
