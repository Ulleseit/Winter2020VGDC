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
    public Skill[] SkillTree = new Skill[]
    {
	new Skill{ amount = 0, active = true, name = "1" , description = "1"}, // 0 RootSkill 
	new Skill{ amount = 0, active = false, name = "2" , description = "2"}, // 1 leftSkillA 
	new Skill{ amount = 0, active = false, name = "3" , description = "3"}, // 2 leftSkillB 
	new Skill{ amount = 0, active = false, name = "4" , description = "4"}, // 3 RightSkillA 
	new Skill{ amount = 0, active = false, name = "5" , description = "5"}, // 4 RightSkillB 
	new Skill{ amount = 0, active = false, name = "6" , description = "6"} // 5 LastSkill 
    };


    
    public Item RHand;
    public Item LHand;
    public Item Armor;

    void Start()
    {
    	for(int i = 0; i < NumSkill; i++)
	{
	    SkillsActive[i] = false;
	}
	SkillsActive[0] = true;
    }
    void Update()
    {
	for(int i = 0; i < NumSkill; i++)
	{
		SkillTree[i].active = SkillsActive[i];
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
    public string printStat(){
    	return " Stats \n" + "initiative: " + initiative + "\nActionPoints: " + maxActionPoints + "\nhealth: " + health + "\narmor:" + armor + "\nstrength: " + strength + "\nmana: " + mana + "\naccuracy: " + accuracy + "\nevasion: " + evasion;
    }
}
