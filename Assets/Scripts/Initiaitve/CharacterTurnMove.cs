using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTurnMove : MonoBehaviour
{
    GameObject[] characters;
    public GameObject selected = null;
    GameObject[] enemies;
    GameObject[] combatMembers;
    // Start is called before the first frame update
    void Start()
    {
      characters = GameObject.FindGameObjectsWithTag("Character");//Set up and update array of GameObjects
      enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
      characters = GameObject.FindGameObjectsWithTag("Character");//Update array of GameObjects
      enemies = GameObject.FindGameObjectsWithTag("Enemy");
      var tempList = new List<GameObject>();
      tempList.AddRange(characters);
      tempList.AddRange(enemies);
      combatMembers = tempList.ToArray();
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
      if(selected.tag == "Character")
      {
        if(Input.GetMouseButtonDown(0))//Check if Player is clicking with a selected character
        {
          bool matching = false;//Initialize check
          for(int x = 0; x < combatMembers.Length; x++)
          {
            if(combatMembers[x].GetComponent<Transform>().position.x == position.x && combatMembers[x].GetComponent<Transform>().position.y == position.y)
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
            selected.GetComponent<MoveCharacter>().move(position.x, position.y);//Move GameObject to selected space
            selected.GetComponent<Stats>().reduceInitiative();
          }

        }
        if(Input.GetKeyDown(KeyCode.W))
        {
          Debug.Log("Turn passed, next in initiative.");
          selected.GetComponent<Stats>().reduceInitiative();
        }
      }
      if(selected.tag == "Enemy")
      {
        Debug.Log("Blegh");
        selected.GetComponent<Stats>().reduceInitiative();
      }
    }
}
