using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTurnOrder : MonoBehaviour
{
    Text text;
    GameObject[] combatMembers;
    void Start()
    {
      text = GetComponent<Text>();
    }
    void Update()
    {
        string x = ("Combat Order: ");
        combatMembers = Camera.main.GetComponent<CharacterTurnMove>().combatMembers;
        for(int i = 0; i < combatMembers.Length; i++)
        {
          x += (combatMembers[i].name + " ");//Adds all ordered members of combat to string
        }
        text.text = x;//Lists which characters are taking their turns in which order
    }
}
