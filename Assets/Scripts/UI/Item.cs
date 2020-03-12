using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour
{
    public enum ItemType
    {
        None,
        Weapon,
        Armor,
        Potion,
    }

    public struct Stats
    {
        public Stats(int health = 0, int armor = 0, int strength = 0, int mana = 0, int accuracy = 0, int evasion = 0)
        {
            this.health = health;
            this.armor = armor;
            this.strength = strength;
            this.mana = mana;
            this.accuracy = accuracy;
            this.evasion = evasion;
        }

        public int health;
        public int armor;
        public int strength;
        public int mana;
        public int accuracy;
        public int evasion;
    }

    public ItemType itemType;

    public int amount = 1;
    public int cost = 0;
    public string name = "Nothing";
    public string description = "There's nothing here";
    public Stats stats;
    // public Sprite sprite;
}
