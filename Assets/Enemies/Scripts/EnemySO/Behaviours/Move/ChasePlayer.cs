using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/MoveLogic/ChasePlayer", order = 0)]
public class ChasePlayer :MoveSOBase
{
    public override void MoveLogic()
    {      
        enemyAgent.destination = base.playerTranform.position;
    }
}
