using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject ObjPrefab;
        public int PoolSize;
    }

    public List<Pool> PoolList;

    public Dictionary<string, List<GameObject>> PoolDictionary;

    
    public static ObjectPooler instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }


    // Start is called before the first frame update
    void Start()
    {
        PoolDictionary = new Dictionary<string, List<GameObject>>();

        foreach(Pool pool in PoolList)
        {
            if ( pool.Tag != null )
            {
                List<GameObject> objectPool = new List<GameObject>();

                for (int i = 0; i < pool.PoolSize; i++)
                {
                    GameObject obj = Instantiate(pool.ObjPrefab);
                    obj.SetActive(false);
                  
                    objectPool.Add(obj);
                }
           
                PoolDictionary.Add(pool.Tag, objectPool);
            }           
        }

    }

    public GameObject TakeFromPool(string tag)
    {
        if (PoolDictionary.ContainsKey(tag))
        {
            foreach (GameObject obj in PoolDictionary[tag])
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }

        }

        return null;
    }

}
