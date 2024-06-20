using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimsList;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/AttackLogic/ProjectileAttack", order = 2)]
public class ProjectileAttack : AttackSOBase
{
    [SerializeField] private GameObject _projectile; 
    //private bool _isAttacking = true;

    public override void AttackLogic(Animator animator, string animation)
    {
        animator.SetFloat("AttackSpeed", 1 / attackSpeed);
        enemyAgent.isStopped = true;
        GameObject projectile = Instantiate(_projectile, bulletPoint.position, Quaternion.identity);
        projectile.transform.parent = enemyObject.transform;
       /* if (_isAttacking)
        {
            animator.SetFloat("AttackSpeed", 1 / attackSpeed);
           // animator.Play(animation);
            enemyAgent.isStopped = true;
            GameObject projectile = Instantiate(_projectile, bulletPoint.position, Quaternion.identity);
            //GameObject projectile = Instantiate(_projectile, enemyObject.transform.position, Quaternion.identity);
            projectile.transform.parent = enemyObject.transform;
           // _isAttacking = false;
           // base.enemy.StartCoroutine(Attack(animator,animation));
        }     */                 
    }
   /* private IEnumerator Attack(Animator anim,string animation)
    {
        anim.Play(animation);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        GameObject projectile = Instantiate(_projectile, bulletPoint.position, Quaternion.identity);
        //GameObject projectile = Instantiate(_projectile, enemyObject.transform.position, Quaternion.identity);
        projectile.transform.parent = enemyObject.transform;
        
        _isAttacking = true ;
       
    }*/
}
