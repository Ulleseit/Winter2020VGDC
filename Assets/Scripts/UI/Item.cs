using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public enum ItemType
    {
        Weapon,
        HeadArmor,
        ChestArmor,
        LegArmor,
        Boots,
        Potion
    }

    public ItemType itemType;
    public int amount;
    public string name;

}
