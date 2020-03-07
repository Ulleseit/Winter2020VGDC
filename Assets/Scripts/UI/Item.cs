using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour
{

    public enum ItemType
    {
        Weapon,
        Armor,
        Potion,
    }

    public ItemType itemType;
    public int amount;
    public int cost;
    public string name;
    public string description;
    public Sprite sprite;

}
