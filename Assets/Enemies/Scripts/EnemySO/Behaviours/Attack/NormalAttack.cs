using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/AttackLogic/NormalAttack", order = 0)]
public class NormalAttack : AttackSOBase
{
    public override void AttackLogic( Animator animator,string animation)
    {
        animator.SetFloat("AttackSpeed", 1 / attackSpeed);
        animator.Play(animation);
    }        
        /*if (Vector3.Distance(transform.position,playerTranform.position) <= attackDistance)
        {
            animator.Play(animation);
            Debug.Log("attack");
        } */       
    
}
