using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Potion,
    }

    public ItemType itemType;

    public int amount = 1;
    public int cost = 0;
    public string name = "Nothing";
    public string description = "There's nothing here";
    public CharacterStats stats;
    // public Sprite sprite;
}
