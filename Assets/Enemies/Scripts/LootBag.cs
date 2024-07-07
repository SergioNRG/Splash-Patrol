using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class LootBag : MonoBehaviour
{
    public List<Loot> LootList = new List<Loot>();

    public static ObjectPool<GameObject> LootPool;

    private void Start()
    {
        LootPool = new ObjectPool<GameObject>(CreateDrop);
    }
    private Loot ChoseDrop()
    {
        int randomChance = Random.Range(1, 101);
        List<Loot> possibleDrop = new List<Loot>();

        foreach (Loot item in LootList)
        {
            if (randomChance <= item.DropChance)
            {
                possibleDrop.Add(item);
            }
        }
        if (possibleDrop.Count > 0)
        {
            Loot drop = possibleDrop[Random.Range(0,possibleDrop.Count)];
            return drop;
        }
        return null;
    }

    public GameObject GetLoot(Vector3 pos)
    {
        var loot = LootPool.Get();
        if (loot!= null)
        {   
            loot.transform.position = pos;
            loot.SetActive(true);
            return loot;
        }else { return null; }
        
    }

    public GameObject CreateDrop()
    {
        Loot drop = ChoseDrop();
        if (drop != null)
        {
            GameObject dropItem = Instantiate(drop.LootPrefab, transform.position,Quaternion.identity); 
            return dropItem;
        }
        return null;
    }
}
