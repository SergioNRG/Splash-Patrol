
using UnityEngine.Pool;
using UnityEngine;

public interface IPooled 
{
    public void SetPool(ObjectPool<GameObject> pool);
}
