using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/EnemyLogic/IdleLogic/JustIdle", order = 0)]
public class JustIdle : IdleSOBase
{
    public override void IdleLogic()
    {
        Debug.Log("do nothing");
    }
}
