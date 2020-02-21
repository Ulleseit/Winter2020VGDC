using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class CharacterTurnMove : MonoBehaviour
{
    GameObject[] characters;
    GameObject selected = null;
    GameObject[] enemies;
  	Tilemap tilemap;
  	public GameObject[] buttons;
  	bool buttonPressed = false;
  	bool menu = false;
    int prevPoints;
    Vector3 prevPos;
    bool endTurn = false;
    void Start()
    {
		characters = GameObject.FindGameObjectsWithTag("Character");//Set up and update array of GameObjects that are player controlled
		enemies = GameObject.FindGameObjectsWithTag("Enemy");//Set up and update array of GameObjects that are AI controlled
		tilemap = this.GetComponent<Tilemap>();
    }
    void Update()
    {
		GameObject[] combatMembers = createCombatMembers();

		Vector3 position = createTileMouse();

		selected = combatMembers[0];
		if(selected.GetComponent<Stats>().currentActionPoints == 0 && endTurn)//When are character reaches zero action points, the turn is ended, and action points refreshed
		{
			selected.GetComponent<Stats>().reduceInitiative();
			selected.GetComponent<Stats>().currentActionPoints = selected.GetComponent<Stats>().maxActionPoints;
      endTurn = false;
		}

		else if(selected.tag == "Character" && !menu)
		{
			if(Input.GetMouseButtonDown(0))//Check if Player is clicking with a selected character
			{
				//Movement Code
				if((int)(Math.Abs(selected.GetComponent<Transform>().position.x - position.x) + Math.Abs(selected.GetComponent<Transform>().position.y - position.y)) > selected.GetComponent<Stats>().currentActionPoints)
				{
					Debug.Log("Too far to move!");
				}
				else
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
            prevPoints = selected.GetComponent<Stats>().currentActionPoints;
            prevPos = selected.GetComponent<Transform>().position;
            selected.GetComponent<Stats>().reduceActionPoints((int)(Math.Abs(selected.GetComponent<Transform>().position.x - position.x) + Math.Abs(selected.GetComponent<Transform>().position.y - position.y)));
						selected.GetComponent<MoveCharacter>().move(position.x, position.y);//Move GameObject to selected space
						foreach(GameObject button in buttons)
						{
							button.SetActive(true);
						}
						menu = true;
					}
					//End Movement Code
					//Options Code

					//End Options Code

				}

			}
			else if(Input.GetKeyDown("space"))//When space is pressed, selected character's turn is passed
			{
				selected.GetComponent<Stats>().reduceInitiative();
			}
		}
		else if(selected.tag == "Character" && menu)
		{
			if(buttonPressed)
			{
				foreach(GameObject button in buttons)
				{
					button.SetActive(false);
				}
				menu = false;
				buttonPressed = false;
			}

		}
		else if(selected.tag == "Enemy")//Enemy movement code will go here in the future
		{
			Debug.Log("Blegh");
			selected.GetComponent<Stats>().reduceInitiative();

		}
	}

	Vector3 createTileMouse()
	{
		Vector3 positionA = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		positionA.z = 0;
		positionA.x = (float)(System.Math.Floor(positionA.x)+.5);
		positionA.y = (float)(System.Math.Floor(positionA.y)+.5);//Make mouse only count at center of each square, instead of true position
		return positionA;
	}

	public GameObject[] createCombatMembers()
	{
		GameObject[] combatMembers;
		characters = GameObject.FindGameObjectsWithTag("Character");//Update array of GameObjects
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		var tempList = new List<GameObject>();//A temporary list in order to combine both arrays
		tempList.AddRange(characters);
		tempList.AddRange(enemies);
		combatMembers = tempList.ToArray();//combatMembers is the final array of all combatants that will be used
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
		return combatMembers;
	}


	public void returnButton()
	{
    selected.GetComponent<Stats>().currentActionPoints = prevPoints;
		selected.GetComponent<Transform>().position = prevPos;
		buttonPressed = true;
	}

	public void endButton()
	{
    endTurn = true;
    selected.GetComponent<Stats>().reduceActionPoints(selected.GetComponent<Stats>().currentActionPoints);
		buttonPressed = true;
	}
}
