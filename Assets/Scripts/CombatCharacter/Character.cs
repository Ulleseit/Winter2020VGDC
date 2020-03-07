using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string Cname;
    public int initiative;
    public int maxActionPoints;
    public int currentActionPoints;
    public Sprite CharSprite;
    // Stats for combat
    public int health;
    public int currentHealth;
    public int armor; // low priority
    public int strength;
    public int mana; // low priority
    public int accuracy; // low priority
    public int evasion; // low priority

    public int NumSkill = 6;
    public bool[] SkillsActive = new bool[6];
    public Skill[] SkillTree = new Skill[6];

    public Character()
    {
        SkillTree[0] = new Skill { amount = 1, active = false, unlocked = true, name = "1", description = "armor" }; // 0 RootSkill 
        SkillTree[1] = new Skill { amount = 2, active = false, unlocked = false, name = "2", description = "strength" }; // 1 leftSkillA 
        SkillTree[2] = new Skill { amount = 3, active = false, unlocked = false, name = "3", description = "mana" }; // 2 leftSkillB 
        SkillTree[3] = new Skill { amount = 4, active = false, unlocked = false, name = "4", description = "accuracy" }; // 3 RightSkillA 
        SkillTree[4] = new Skill { amount = 5, active = false, unlocked = false, name = "5", description = "evasion" }; // 4 RightSkillB 
        SkillTree[5] = new Skill { amount = 6, active = false, unlocked = false, name = "6", description = "health" }; // 5 LastSkill 

        for (int i = 0; i < NumSkill; i++)
        {
            SkillsActive[i] = false;
        }
    }

    public Item RHand = new Item { itemType = Item.ItemType.None, stats = new Item.Stats() };
    public Item LHand = new Item { itemType = Item.ItemType.None, stats = new Item.Stats() };
    public Item Armor = new Item { itemType = Item.ItemType.None, stats = new Item.Stats() };

    void Start()
    {
        for (int i = 0; i < NumSkill; i++)
        {
            SkillsActive[i] = false;
        }
    }

    public void activateSkill(int number)
    {
        if (SkillTree[number].active == false)
        {
            SkillTree[number].active = true;

            switch (SkillTree[number].description)
            {
                case "initiative":
                    initiative += SkillTree[number].amount;
                    break;
                case "maxActionPoints":
                    maxActionPoints += SkillTree[number].amount;
                    break;
                case "health":
                    health += SkillTree[number].amount;
                    break;
                case "armor":
                    armor += SkillTree[number].amount;
                    break;
                case "strength":
                    strength += SkillTree[number].amount;
                    break;
                case "mana":
                    mana += SkillTree[number].amount;
                    break;
                case "accuracy":
                    accuracy += SkillTree[number].amount;
                    break;
                case "evasion":
                    evasion += SkillTree[number].amount;
                    break;
                default:
                    Debug.Log("Invalid description");
                    break;
            }
        }
    }

    public void reduceInitiative()//Used to reduce initiative at the end of the turn, should initiatives ever end up higher than 100, change value to minus 1000 unless high initiative means taking an extra turn
    {
        initiative -= 100;
    }

    public void reduceActionPoints(int n)//Used to reduce action points, given an int to reduce by
    {
        currentActionPoints -= n;
    }

    public string printStat()
    {
        return " Stats \n" + "initiative: " + initiative + "\nActionPoints: " + maxActionPoints + "\nhealth: " + health + "\narmor:" + armor + "\nstrength: " + strength + "\nmana: " + mana + "\naccuracy: " + accuracy + "\nevasion: " + evasion;
    }

    public void equipItem(string slot, Item item)
    {
        switch (slot)
        {
            case "rhand":
                RHand = item;
                break;
            case "lhand":
                LHand = item;
                break;
            case "armor":
                Armor = item;
                break;
            default:
                Debug.Log("Invalid slot");
                return;
        }

        health += item.stats.health;
        armor += item.stats.armor;
        strength += item.stats.strength;
        mana += item.stats.mana;
        accuracy += item.stats.accuracy;
        evasion += item.stats.evasion;
    }

    public void unequipItem(string slot)
    {
        switch (slot)
        {
            case "rhand":
                health -= RHand.stats.health;
                armor -= RHand.stats.armor;
                strength -= RHand.stats.strength;
                mana -= RHand.stats.mana;
                accuracy -= RHand.stats.accuracy;
                evasion -= RHand.stats.evasion;
                RHand = new Item { itemType = Item.ItemType.None, stats = new Item.Stats() };
                break;
            case "lhand":
                health -= LHand.stats.health;
                armor -= LHand.stats.armor;
                strength -= LHand.stats.strength;
                mana -= LHand.stats.mana;
                accuracy -= LHand.stats.accuracy;
                evasion -= LHand.stats.evasion;
                LHand = new Item { itemType = Item.ItemType.None, stats = new Item.Stats() };
                break;
            case "armor":
                health -= Armor.stats.health;
                armor -= Armor.stats.armor;
                strength -= Armor.stats.strength;
                mana -= Armor.stats.mana;
                accuracy -= Armor.stats.accuracy;
                evasion -= Armor.stats.evasion;
                Armor = new Item { itemType = Item.ItemType.None, stats = new Item.Stats() };
                break;
            default:
                Debug.Log("Invalid slot");
                return;
        }
    }
}
