using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/AttackLogic/CloseRangeAttack", order = 0)]
public class CloseRangeAttack : AttackSOBase
{
    public override void AttackLogic()
    {
        if (Vector3.Distance(transform.position,playerTranform.position) <= 2)
        {
            Debug.Log("attack");
        }        
    }
}
