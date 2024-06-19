using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/AttackLogic/ProjectileAttack", order = 2)]
public class ProjectileAttack : AttackSOBase
{
    [SerializeField] private GameObject _projectile;
    private bool _isAttacking = true;

    public override void AttackLogic(Animator animator, string animation)
    {
        if (_isAttacking)
        {
            animator.Play(animation);
            
            _isAttacking = false;
            base.enemy.StartCoroutine(Attack());
        }                      
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        GameObject projectile = Instantiate(_projectile, bulletPoint.position, Quaternion.identity);
        //GameObject projectile = Instantiate(_projectile, enemyObject.transform.position, Quaternion.identity);
        projectile.transform.parent = enemyObject.transform;
        _isAttacking = true ;
    }
}
