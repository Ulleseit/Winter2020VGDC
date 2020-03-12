using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modifiedSlimeCharacter : MonoBehaviour
{
    public string Cname;
	public int level = 1;
	public int experience = 0;
    public int initiative;
    public int maxActionPoints = 5;
    public int currentActionPoints = 5;
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

    void Start()
    {
		health = 10 + (int)UnityEngine.Random.Range(0.0f, 5.0f) * 5;
		currentHealth = health;
		strength = 5 + (int)UnityEngine.Random.Range(0.0f, 5.0f) * 5;
		evasion = UnityEngine.Random.Range(0.3f, 0.5f);
    }

 
    public void reduceInitiative()//Used to reduce initiative at the end of the turn, should initiatives ever end up higher than 100, change value to minus 1000 unless high initiative means taking an extra turn
    {
        initiative -= 100;
    }

    public void reduceActionPoints(int n)//Used to reduce action points, given an int to reduce by
    {
        currentActionPoints -= n;
    }




    
}
