using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimsList;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/AttackLogic/ProjectileAttack", order = 2)]
public class ProjectileAttack : AttackSOBase
{
   // [SerializeField] private GameObject _projectile; 
    //private bool _isAttacking = true;

    public override void AttackLogic(Animator animator)
    {
        animator.SetFloat("AttackSpeed", 1 * attackSpeed);
        enemyAgent.isStopped = true;
        GameObject projectile = Instantiate(base.projectile, bulletPoint.position, Quaternion.identity);
        projectile.transform.parent = enemyObject.transform;        
    }

}
