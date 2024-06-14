using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/MoveLogic/ChasePlayer", order = 0)]
public class ChasePlayer : MoveSOBase
{

    public override void MoveLogic()
    {
        // base.enemyObject.transform.position = Vector3.MoveTowards(base.enemyObject.transform.position, base.playerTranform.position, base.enemy.MovSpeed * Time.deltaTime);
        enemyAgent.destination = base.playerTranform.position;
    }
}
