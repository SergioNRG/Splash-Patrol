using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public interface IPooled 
{
    // private ObjectPool
    public void SetPool(ObjectPool<GameObject> pool);//,Transform trans);
}
