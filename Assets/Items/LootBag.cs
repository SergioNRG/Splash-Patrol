using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public List<Loot> LootList = new List<Loot>();

    private Loot GetDrop()
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

    public void instantiateDrop(Vector3 position)
    {
        Loot drop = GetDrop();
        if (drop != null)
        {
            GameObject dropItem = Instantiate(drop.LootPrefab, position,Quaternion.identity); 
        }
    }
}
