
using UnityEngine;

public abstract class WeaponSOBase : ScriptableObject
{
    public abstract void Attack();
    public abstract void Spawn(Transform Parent, MonoBehaviour activeMonoBehaviour);
}
