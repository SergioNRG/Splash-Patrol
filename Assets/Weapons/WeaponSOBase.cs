using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSOBase : ScriptableObject
{
    public abstract void Attack();
    public abstract void Spawn(Transform Parent, MonoBehaviour activeMonoBehaviour);
}
