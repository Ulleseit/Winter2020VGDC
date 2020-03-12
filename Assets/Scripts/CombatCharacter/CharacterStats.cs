using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public int health;
    public int currentHealth;
    public int armor; 
    public int strength;
    public int mana; 
    public int accuracy; 
    public int evasion; 
    public int damage;

    public CharacterStats(int health = 0, int currentHealth = 0, int armor = 0, int strength = 0, int mana = 0, int accuracy = 0, int evasion = 0)
    {
        this.health = health;
        this.currentHealth = currentHealth;
        this.armor = armor;
        this.strength = strength;
        this.mana = mana;
        this.accuracy = accuracy;
        this.evasion = evasion;
    }

    public static CharacterStats operator +(CharacterStats a, CharacterStats b)
    {
        return new CharacterStats(
            a.health + b.health,
            a.currentHealth + b.currentHealth,
            a.armor + b.armor,
            a.strength + b.strength,
            a.mana + b.mana,
            a.accuracy + b.accuracy,
            a.evasion + b.evasion
        );
    }

    public static CharacterStats operator -(CharacterStats a, CharacterStats b)
    {
        return new CharacterStats(
            a.health - b.health,
            a.currentHealth - b.currentHealth,
            a.armor - b.armor,
            a.strength - b.strength,
            a.mana - b.mana,
            a.accuracy - b.accuracy,
            a.evasion - b.evasion
        );
    }
}
