using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;
using static AnimsList;
using TMPro;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/AttackLogic/ProjectileAttack", order = 2)]
public class ProjectileAttack : AttackSOBase
{
    public static ObjectPool<GameObject> Pool;

    private void OnEnable()
    {
        Pool = new ObjectPool<GameObject>(CreateProjectile);
    }


    public override void AttackLogic(Animator animator)
    {
        animator.SetFloat("AttackSpeed", 1 * attackSpeed);
        enemyAgent.isStopped = true;
        GetProjectile();      
    }

    public GameObject GetProjectile()
    {
        var projectile = Pool.Get();
        var rb = projectile.GetComponent<Rigidbody>();
        projectile.transform.position = bulletPoint.position;
        projectile.SetActive(true);

        return projectile;
    }



    private GameObject CreateProjectile()
    {
        GameObject projectille = Instantiate(base.projectile, bulletPoint.position, Quaternion.identity);
 
        projectille.GetComponent<IPooled>().SetPool(Pool);
        projectille.GetComponent<IProjectile>().SetProjectileForce(projectileForce);

        return projectille;
    }
}
