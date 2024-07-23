using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/MoveLogic/MoveToPortal", order = 2)]
public class MoveToPortal : MoveSOBase
{
    public override void MoveLogic()
    {
        
        enemyAgent.destination = base.portalTranform.position;
    }
}
