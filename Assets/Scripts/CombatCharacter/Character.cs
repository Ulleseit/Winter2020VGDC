using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string Cname;
	public int level = 1;
	public int experience = 0;
    public int initiative = 50;
	public int curInitiative;
    public int maxActionPoints = 5;
    public int currentActionPoints = 5;
    public Sprite CharSprite;

    public CharacterStats stats = new CharacterStats();

    public int Health { 
        get { return stats.health; } 
        set { stats.health = value; }
    }
    public int CurrentHealth {
        get { return stats.currentHealth; }
        set { stats.currentHealth = value; }
    }
    public int Armor {
        get { return stats.armor; }
        set { stats.armor = value; }
    }
    public int Strength {
        get { return stats.strength; }
        set { stats.strength = value; }
    }
    public int Mana {
        get { return stats.strength; }
        set { stats.mana = value; }
    }
    public int Accuracy {
        get { return stats.accuracy; }
        set { stats.accuracy = value; }
    }
    public int Evasion {
        get { return stats.evasion; }
        set { stats.evasion = value; }
    }
    public int Damage {
        get { return stats.damage; }
        set { stats.damage = value; }
    }



    public int NumSkill = 6;
    public bool[] SkillsActive = new bool[6];
    public Skill[] SkillTree = new Skill[6];

    public Character()
    {
        SkillTree[0] = new Skill { amount = 1, active = false, unlocked = true, name = "1", description = "armor", statModifier = new CharacterStats(health: 1) }; // 0 RootSkill 
        SkillTree[1] = new Skill { amount = 2, active = false, unlocked = false, name = "2", description = "strength", statModifier = new CharacterStats(strength: 2) }; // 1 leftSkillA 
        SkillTree[2] = new Skill { amount = 3, active = false, unlocked = false, name = "3", description = "mana", statModifier = new CharacterStats(strength: 3) }; // 2 leftSkillB 
        SkillTree[3] = new Skill { amount = 4, active = false, unlocked = false, name = "4", description = "accuracy", statModifier = new CharacterStats(health: 4) }; // 3 RightSkillA 
        SkillTree[4] = new Skill { amount = 5, active = false, unlocked = false, name = "5", description = "evasion", statModifier = new CharacterStats(strength: 5) }; // 4 RightSkillB 
        SkillTree[5] = new Skill { amount = 6, active = false, unlocked = false, name = "6", description = "health", statModifier = new CharacterStats(health: 6) }; // 5 LastSkill 

        for (int i = 0; i < NumSkill; i++)
        {
            SkillsActive[i] = false;
        }
    }

    public Item RHandSlot = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };
    public Item LHandSlot = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };
    public Item ArmorSlot = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };

    void Start()
    {
        for (int i = 0; i < NumSkill; i++)
        {
            SkillsActive[i] = false;
        }
		if(gameObject.name == "Spider")
		{
			Health = 8;
			CurrentHealth = Health;
			Damage = 4;
			initiative = 25;
			curInitiative = 25;
		}
		else if(gameObject.name == "Slime")
		{
			Health = 12;
			CurrentHealth = Health;
			Damage = 3;
			initiative = 25;
			curInitiative = 25;
		}
		else
		{
			Health = 10 + 5*level;
			CurrentHealth = Health;
			Strength = 5 + 5*level;
			curInitiative = initiative;
		}
    }

	void Update()
	{
		if(CurrentHealth <= 0)
		{
			Destroy(gameObject);
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
        curInitiative -= 100;
    }

    public void reduceActionPoints(int n)//Used to reduce action points, given an int to reduce by
    {
        currentActionPoints -= n;
    }

    public string printStat()
    {
        return gameObject.name + "\nLevel: " + level + "\nExperience: " + experience + "/" + level*125 + "\nHealth: " + Health + "\nStrength: " + Strength + "\nInitiative: " + initiative + "\nActionPoints: " + maxActionPoints;
    }

    public void equipItem(string slot, Item item)
    {
        switch (slot)
        {
            case "rhand":
                RHandSlot = item;
                break;
            case "lhand":
                LHandSlot = item;
                break;
            case "armor":
                ArmorSlot = item;
                break;
            default:
                Debug.Log("Invalid slot");
                return;
        }
	Debug.Log(item.name);
        //stats = stats + item.stats;
    }

    public void unequipItem(string slot)
    {
        switch (slot)
        {
            case "rhand":
                stats -= RHandSlot.stats;
                RHandSlot = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };
                break;
            case "lhand":
                stats -= LHandSlot.stats;
                LHandSlot = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };
                break;
            case "armor":
                stats -= ArmorSlot.stats;
                ArmorSlot = new Item { itemType = Item.ItemType.Weapon, stats = new CharacterStats() };
                break;
            default:
                Debug.Log("Invalid slot");
                return;
        }
    }
}
