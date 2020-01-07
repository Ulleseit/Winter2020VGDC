using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterTurnMove : MonoBehaviour
{
    GameObject[] characters;
    GameObject selected = null;
    GameObject[] enemies;
    public GameObject[] combatMembers;
    void Start()
    {
      characters = GameObject.FindGameObjectsWithTag("Character");//Set up and update array of GameObjects that are player controlled
      enemies = GameObject.FindGameObjectsWithTag("Enemy");//Set up and update array of GameObjects that are AI controlled
    }
    void Update()
    {
      characters = GameObject.FindGameObjectsWithTag("Character");//Update array of GameObjects
      enemies = GameObject.FindGameObjectsWithTag("Enemy");
      var tempList = new List<GameObject>();//A temporary list in order to combine both arrays
      tempList.AddRange(characters);
      tempList.AddRange(enemies);
      combatMembers = tempList.ToArray();//combatMembers is the final array of all combatants that will be used
      Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      position.z = 0;
      position.x = (float)(System.Math.Floor(position.x)+.5);
      position.y = (float)(System.Math.Floor(position.y)+.5);//Make mouse only count at center of each square, instead of true position
      for(int n = 0; n < combatMembers.Length-1; n++)
      {
        int greatestPosition = n;
        for(int m = n+1; m < combatMembers.Length; m++)
        {
          if(combatMembers[m].GetComponent<Stats>().initiative > combatMembers[greatestPosition].GetComponent<Stats>().initiative)
          {
            greatestPosition = m;
          }
        }
        GameObject temp = combatMembers[greatestPosition];
        combatMembers[greatestPosition] = combatMembers[n];
        combatMembers[n] = temp;
      }
      selected = combatMembers[0];
      if(selected.GetComponent<Stats>().currentActionPoints == 0)//When are character reaches zero action points, the turn is ended, and action points refreshed
      {
        selected.GetComponent<Stats>().reduceInitiative();
        selected.GetComponent<Stats>().currentActionPoints = selected.GetComponent<Stats>().maxActionPoints;
      }
      else if(selected.tag == "Character")
      {
        if(Input.GetMouseButtonDown(0))//Check if Player is clicking with a selected character
        {
          bool matching = false;//Initialize check
          for(int x = 0; x < combatMembers.Length; x++)
          {
            if(combatMembers[x].GetComponent<Transform>().position.x == position.x && combatMembers[x].GetComponent<Transform>().position.y == position.y)//Checks if any GameObjects are in the selected position to avoid collision
            {
              matching = true;//If any GameObjects are in the position of selected movement location, change matching to true
            }
          }
          if(matching)//If there is a GameObject in position, don't allow movement
          {
            Debug.Log("Same position as another character, can not move here!");
          }
          else if(!matching)
          {
            if((int)(Math.Abs(selected.GetComponent<Transform>().position.x - position.x) + Math.Abs(selected.GetComponent<Transform>().position.y - position.y)) > selected.GetComponent<Stats>().currentActionPoints)//Checks if the current action points are enough to move the player to a location
            {
              Debug.Log("That is too far to move");
            }
            else
            {
              selected.GetComponent<Stats>().reduceActionPoints((int)(Math.Abs(selected.GetComponent<Transform>().position.x - position.x) + Math.Abs(selected.GetComponent<Transform>().position.y - position.y)));//Spend AP to move squares
              selected.GetComponent<MoveCharacter>().move(position.x, position.y);//Move GameObject to selected space

            }
          }

        }
        if(Input.GetKeyDown("space"))//When space is pressed, selected character's turn is passed
        {
          selected.GetComponent<Stats>().reduceInitiative();
        }
      }
      else if(selected.tag == "Enemy")//Enemy movement code will go here in the future
      {
        Debug.Log("Blegh");
        selected.GetComponent<Stats>().reduceInitiative();
      }
    }
}
