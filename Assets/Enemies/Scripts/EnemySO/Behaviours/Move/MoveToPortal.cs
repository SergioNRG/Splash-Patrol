using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/MoveLogic/MoveToPortal", order = 2)]
public class MoveToPortal : MoveSOBase
{
    public override void MoveLogic()
    {
        // base.enemyObject.transform.position = Vector3.MoveTowards(base.enemyObject.transform.position, base.playerTranform.position, base.enemy.MovSpeed * Time.deltaTime);
        enemyAgent.destination = base.portalTranform.position;
    }
}
