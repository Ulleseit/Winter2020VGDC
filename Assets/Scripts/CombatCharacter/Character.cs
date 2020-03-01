﻿using System.Collections;
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
    public int armor; // low priority
    public int strength;
    public int mana; // low priority
    public int accuracy; // low priority
    public int evasion; // low priority

    public Item RHand;
    public Item LHand;
    public Item Armor;

    void Start()
    {

    }
    void Update()
    {

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