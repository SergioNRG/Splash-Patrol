using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;
using static AnimsList;
using TMPro;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/AttackLogic/ProjectileAttack", order = 2)]
public class ProjectileAttack : AttackSOBase
{
    // [SerializeField] private GameObject _projectile; 
    //private bool _isAttacking = true;

    public static ObjectPool<GameObject> Pool;

    private void OnEnable()
    {
        Pool = new ObjectPool<GameObject>(CreateProjectile);
    }


    public override void AttackLogic(Animator animator)
    {
        animator.SetFloat("AttackSpeed", 1 * attackSpeed);
        enemyAgent.isStopped = true;
        GetBossRock();
        //GameObject projectile = Instantiate(base.projectile, bulletPoint.position, Quaternion.identity);
        //projectile.transform.parent = enemyObject.transform;        
    }

    public GameObject GetBossRock()
    {
        var rock = Pool.Get();
        rock.transform.position = bulletPoint.position;
        rock.SetActive(true);
        var rb = rock.GetComponent<Rigidbody>();
        Debug.Log(rb);
        rb.AddForce(transform.forward * 15, ForceMode.Impulse);
        //rock.transform.parent = enemyObject.transform;
        return rock;
    }



    private GameObject CreateProjectile()
    {
        GameObject projectile = Instantiate(base.projectile, bulletPoint.position, Quaternion.identity);
        //var rb = projectile.GetComponent<Rigidbody>();
        
        projectile.GetComponent<IPooled>().SetPool(Pool);//,bulletPoint);
        return projectile;
    }
}
