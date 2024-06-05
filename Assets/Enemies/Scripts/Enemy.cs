using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : EnemyBase
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }

    protected override void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, base._target.position, base._movSpeed* Time.deltaTime);
    }
    protected override void Attack()
    {
        Debug.Log(" Atacando");
    }
}
