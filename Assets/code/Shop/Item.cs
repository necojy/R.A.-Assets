using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static Item Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public enum ItemType
    {
        HealthPotion,
        Skill
          
    }
    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.HealthPotion: return 5;
            case ItemType.Skill: return 10;
        }
    }
}
