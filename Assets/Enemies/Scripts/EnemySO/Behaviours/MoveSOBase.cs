using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSOBase : ScriptableObject
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

    public virtual void MoveLogic() { }
}
