using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/AttackLogic/NormalAttack", order = 0)]
public class NormalAttack : AttackSOBase
{
    public override void AttackLogic(int attackDistance, Animator animator,string animation)
    {
        if (Vector3.Distance(transform.position,playerTranform.position) <= attackDistance)
        {
            animator.Play(animation);
            Debug.Log("attack");
        }        
    }
}
