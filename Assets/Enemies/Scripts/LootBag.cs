using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class LootBag : MonoBehaviour
{
    public List<Loot> LootList = new List<Loot>();


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


    public void SpawnLoot(Transform trans)
    {
        Loot drop = ChoseDrop();
        if (drop != null)
        {
            Debug.Log(drop.name);
            GameObject itemToDrop = LootPool.instance.TakeFromPool(drop.name);
            itemToDrop.transform.position = trans.position;
            itemToDrop.SetActive(true);
        }
    }
}
