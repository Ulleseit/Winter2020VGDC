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
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    bool cameraLock = true;
    float horizontalResolution = 1920;
    void Start()
    {
		characters = GameObject.FindGameObjectsWithTag("Character");//Set up and update array of GameObjects that are player controlled
		enemies = GameObject.FindGameObjectsWithTag("Enemy");//Set up and update array of GameObjects that are AI controlled
		tilemap = this.GetComponent<Tilemap>();
    float currentAspect = (float) Screen.width / (float) Screen.height;
    Camera.main.orthographicSize = horizontalResolution / currentAspect / 400;
    }
    void Update()
    {
    Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, minX, maxX), Mathf.Clamp(Camera.main.transform.position.y, minY, maxY), -10);
		GameObject[] combatMembers = createCombatMembers();
    selected = combatMembers[0];
		Vector3 position = createTileMouse();
    if(cameraLock)
    {
      Camera.main.transform.position = new Vector3(Mathf.Clamp(selected.GetComponent<Transform>().position.x, minX, maxX), Mathf.Clamp(selected.GetComponent<Transform>().position.y, minY, maxY), -10);
      Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);

    }

    if(Input.GetKey("w"))
    {
      Camera.main.transform.Translate(0, .1f, 0);
      cameraLock = false;
    }
    else if(Input.GetKey("a"))
    {
      Camera.main.transform.Translate(-.1f, 0, 0);
      cameraLock = false;
    }
    else if(Input.GetKey("s"))
    {
      Camera.main.transform.Translate(0, -.1f, 0);
      cameraLock = false;
    }
    else if(Input.GetKey("d"))
    {
      Camera.main.transform.Translate(.1f, 0, 0);
      cameraLock = false;
    }
    else if(Input.GetKeyDown("space"))
    {
      cameraLock = true;
    }

		if(selected.GetComponent<Character>().currentActionPoints == 0 && endTurn)//When are character reaches zero action points, the turn is ended, and action points refreshed
		{
			selected.GetComponent<Character>().reduceInitiative();
			selected.GetComponent<Character>().currentActionPoints = selected.GetComponent<Character>().maxActionPoints;
      endTurn = false;
		}
		else if(selected.tag == "Character" && !menu)
		{
			if(Input.GetMouseButtonDown(0))//Check if Player is clicking with a selected character
			{
				//Movement Code
        cameraLock = false;
				if((int)(Math.Abs(selected.GetComponent<Transform>().position.x - position.x) + Math.Abs(selected.GetComponent<Transform>().position.y - position.y)) > selected.GetComponent<Character>().currentActionPoints)
				{
					Debug.Log("Too far to move!");
				}
				else
				{
					bool matching = false;//Initialize check
					for(int x = 0; x < combatMembers.Length; x++)
					{
						if(combatMembers[x].GetComponent<Transform>().position.x == position.x && combatMembers[x].GetComponent<Transform>().position.y == position.y && combatMembers[x] != selected)//Checks if any GameObjects are in the selected position to avoid collision
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
            prevPoints = selected.GetComponent<Character>().currentActionPoints;
            prevPos = selected.GetComponent<Transform>().position;
            selected.GetComponent<Character>().reduceActionPoints((int)(Math.Abs(selected.GetComponent<Transform>().position.x - position.x) + Math.Abs(selected.GetComponent<Transform>().position.y - position.y)));
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
        cameraLock = true;
			}
      else if(Input.GetKeyDown("escape"))
      {
        returnButton();
      }
      else if(Input.GetKeyDown("return"))
      {
        endButton();
      }

		}
		else if(selected.tag == "Enemy")//Enemy movement code will go here in the future
		{
			Debug.Log("Blegh");
			selected.GetComponent<Character>().reduceInitiative();

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
				if(combatMembers[m].GetComponent<Character>().initiative > combatMembers[greatestPosition].GetComponent<Character>().initiative)
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
    selected.GetComponent<Character>().currentActionPoints = prevPoints;
		selected.GetComponent<Transform>().position = prevPos;
		buttonPressed = true;
	}

	public void endButton()
	{
    endTurn = true;
    selected.GetComponent<Character>().reduceActionPoints(selected.GetComponent<Character>().currentActionPoints);
		buttonPressed = true;
	}

}
