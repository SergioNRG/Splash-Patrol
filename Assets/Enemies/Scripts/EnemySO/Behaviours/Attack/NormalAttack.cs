using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/AttackLogic/NormalAttack", order = 0)]
public class NormalAttack : AttackSOBase
{
    public override void AttackLogic( Animator animator)
    {
        animator.SetFloat("AttackSpeed", 1 * attackSpeed);
    }        
             
    
}
