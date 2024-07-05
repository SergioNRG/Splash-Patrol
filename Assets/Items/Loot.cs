using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Item", menuName = "Loot Item", order = 0)]
public class Loot : ScriptableObject
{
    public GameObject LootPrefab;
    public string LootName;
    public int DropChance;

    public Loot(string name, int dropChance)
    {
        this.LootName = name;
        this.DropChance = dropChance;
    }
}
