using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject enemyObject;

    protected Transform playerTranform;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.enemy = enemy;
        this.enemyObject = gameObject;
        this.transform = gameObject.transform;

        playerTranform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void IdleLogic() { }
}
