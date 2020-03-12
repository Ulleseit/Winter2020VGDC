using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string Cname;
	public int level = 1;
	public int experience = 0;
    public int initiative = 50;
    public int maxActionPoints = 5;
    public int currentActionPoints = 5;
    public Sprite CharSprite;

    public CharacterStats stats = new CharacterStats();

    public int NumSkill = 6;
    public bool[] SkillsActive = new bool[6];
    public Skill[] SkillTree = new Skill[6];

    public Character()
    {
        SkillTree[0] = new Skill { amount = 1, active = false, unlocked = true, name = "1", description = "armor", statModifier = new CharacterStats(armor: 1) }; // 0 RootSkill 
        SkillTree[1] = new Skill { amount = 2, active = false, unlocked = false, name = "2", description = "strength", statModifier = new CharacterStats(strength: 2) }; // 1 leftSkillA 
        SkillTree[2] = new Skill { amount = 3, active = false, unlocked = false, name = "3", description = "mana", statModifier = new CharacterStats(mana: 3) }; // 2 leftSkillB 
        SkillTree[3] = new Skill { amount = 4, active = false, unlocked = false, name = "4", description = "accuracy", statModifier = new CharacterStats(accuracy: 4) }; // 3 RightSkillA 
        SkillTree[4] = new Skill { amount = 5, active = false, unlocked = false, name = "5", description = "evasion", statModifier = new CharacterStats(evasion: 5) }; // 4 RightSkillB 
        SkillTree[5] = new Skill { amount = 6, active = false, unlocked = false, name = "6", description = "health", statModifier = new CharacterStats(health: 6) }; // 5 LastSkill 

        for (int i = 0; i < NumSkill; i++)
        {
            SkillsActive[i] = false;
        }
    }

    public Item RHand = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };
    public Item LHand = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };
    public Item Armor = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };

    void Start()
    {
        for (int i = 0; i < NumSkill; i++)
        {
            SkillsActive[i] = false;
        }
		if(gameObject.name == "Spider")
		{
			stats.health = 8;
			stats.currentHealth = stats.health;
			stats.damage = 4;
		}
		else
		{
			stats.health = 10 + 5*level;
			stats.currentHealth = stats.health;
			stats.strength = 5 + 5*level;
		}
    }

    public void activateSkill(int number)
    {
        if (SkillTree[number].active == false)
        {
            SkillTree[number].active = true;

            stats += SkillTree[number].statModifier;
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

        stats += item.stats;
    }

    public void unequipItem(string slot)
    {
        switch (slot)
        {
            case "rhand":
                stats -= RHand.stats;
                RHand = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };
                break;
            case "lhand":
                stats -= LHand.stats;
                LHand = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };
                break;
            case "armor":
                stats -= Armor.stats;
                Armor = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };
                break;
            default:
                Debug.Log("Invalid slot");
                return;
        }
    }
}
