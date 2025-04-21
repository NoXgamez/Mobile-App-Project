using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class LootBox : MonoBehaviour
{
    //put desired items in the list appropriate to its rarities 
    //in the pull method add a method so it adds the item  
    public List<GameObject> common = new List<GameObject>();
    float commonChance = 50;
    public List<GameObject> uncommmon = new List<GameObject>();
    float uncommonChance = 20;
    public List<GameObject> rare = new List<GameObject>();
    float rareChance = 10;
    public List<GameObject> epic = new List<GameObject>();
    float epicChance = 0.1f;
    public List<GameObject> legendary = new List<GameObject>();
    float legendaryChance = 0.001f;
    GameObject pulledItem;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pull();
        }
    }
    public void pull()
    {
        float chance = Random.Range(legendaryChance, 100);
        int listChance;
        if (chance <= legendaryChance && legendary.Count > 0)
        {
            listChance = Random.Range(0, legendary.Count);
            pulledItem = legendary[listChance];
            Debug.Log("Legendary! " + pulledItem.name);
        }
        else if (chance < epicChance && epic.Count > 0)
        {
            listChance = Random.Range(0, epic.Count);
            pulledItem = epic[listChance];
            Debug.Log("Epic! " + pulledItem.name);
        }
        else if (chance < rareChance && rare.Count > 0)
        {
            listChance = Random.Range(0, rare.Count);
            pulledItem = rare[listChance];
            Debug.Log("Rare! " + pulledItem.name);
        }
        else if (chance < uncommonChance && uncommmon.Count > 0)
        {
            listChance = Random.Range(0, uncommmon.Count);
            pulledItem = uncommmon[listChance];
            Debug.Log("Uncommon! " + pulledItem.name);
        }
        else if (common.Count > 0)
        {
            listChance = Random.Range(0, common.Count);
            pulledItem = common[listChance];
            Debug.Log("Common! " + pulledItem.name);
        }
        else
        {
            Debug.LogWarning("No items available in any rarity tier!");
            pulledItem = null;
        }




    }
}
