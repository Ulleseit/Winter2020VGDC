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
	GameObject[] combatMembers;
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
	  bool selectAttack = false;
	  bool selectSkill = false;
    bool cameraLock = true;
  	int range = 1;
  	public TileBase grassLandsAttack;
  	public TileBase mountainAttack;
  	public TileBase grassLands;
  	public TileBase mountain;
    bool moving = false;
	public GameObject chatText;
	public bool running = true;
    void Start()
    {
		characters = GameObject.FindGameObjectsWithTag("Character");//Set up and update array of GameObjects that are player controlled
		enemies = GameObject.FindGameObjectsWithTag("Enemy");//Set up and update array of GameObjects that are AI controlled
		tilemap = this.GetComponent<Tilemap>();

    }
    void Update()
    {
    if(!moving && running)
    {
		Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, minX, maxX), Mathf.Clamp(Camera.main.transform.position.y, minY, maxY), -10);
		combatMembers = createCombatMembers();
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
			cameraLock = true;
		}
		else if(selected.tag == "Character" && !menu)
		{
			if(Input.GetMouseButtonDown(0))//Check if Player is clicking with a selected character
			{
				//Movement Code
				if((int)(Math.Abs(selected.GetComponent<Transform>().position.x - position.x) + Math.Abs(selected.GetComponent<Transform>().position.y - position.y)) > selected.GetComponent<Character>().currentActionPoints)
				{
					Debug.Log("Too far to move!");
				}
				else
				{
					bool matching = false;//Initialize check
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					if(Physics.Raycast(ray, out hit))
					{
						if(hit.transform != selected.transform)
						{
							matching = true;
						}
					}
					if(matching)//If there is a GameObject in position, don't allow movement
					{
						Debug.Log("Same position as another character, can not move here!");
					}

					else if(!matching)
					{
						prevPos = selected.GetComponent<Transform>().position;
						prevPoints = selected.GetComponent<Character>().currentActionPoints;
						List<Point> p = stepTowardsPath(position.x, position.y);
            StartCoroutine(stepThroughPath(p));
						foreach(GameObject button in buttons)
						{
							button.SetActive(true);
						}
						menu = true;
					}
				}

			}
		}
		else if(selected.tag == "Character" && menu && !selectAttack && !selectSkill)
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
		else if(selected.tag == "Character" && menu && selectSkill && !selectAttack)
		{
			Debug.Log("No skills yet sorry");
			selectSkill = false;
		}
		else if(selected.tag == "Character" && menu && selectAttack && !selectSkill)
		{
			if(Input.GetMouseButtonDown(0) && (range == 1 && ((position.x == selected.GetComponent<Transform>().position.x+1 && position.y == selected.GetComponent<Transform>().position.y) || (position.x == selected.GetComponent<Transform>().position.x-1 && position.y == selected.GetComponent<Transform>().position.y) || (position.x == selected.GetComponent<Transform>().position.x && position.y == selected.GetComponent<Transform>().position.y+1) || (position.x == selected.GetComponent<Transform>().position.x && position.y == selected.GetComponent<Transform>().position.y-1))))
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(ray, out hit) && hit.transform.tag == "Enemy")
				{
					hit.transform.gameObject.GetComponent<Character>().CurrentHealth = hit.transform.gameObject.GetComponent<Character>().CurrentHealth - selected.GetComponent<Character>().Strength/2;
					chatText.GetComponent<textToChat>().processLine(selected.name + " dealt " + selected.GetComponent<Character>().Strength/2 + " damage to " + hit.transform.gameObject.name);
					selectAttack = false;
					endButton();
				}
			}
			else if(Input.GetMouseButtonDown(0) && (range == 2 && ((position.x == selected.GetComponent<Transform>().position.x+1 && position.y == selected.GetComponent<Transform>().position.y) || (position.x == selected.GetComponent<Transform>().position.x-1 && position.y == selected.GetComponent<Transform>().position.y) || (position.x == selected.GetComponent<Transform>().position.x && position.y == selected.GetComponent<Transform>().position.y+1) || (position.x == selected.GetComponent<Transform>().position.x && position.y == selected.GetComponent<Transform>().position.y-1)) || (position.x == selected.GetComponent<Transform>().position.x && position.y == selected.GetComponent<Transform>().position.y-2) || (position.x == selected.GetComponent<Transform>().position.x && position.y == selected.GetComponent<Transform>().position.y+2) || (position.x == selected.GetComponent<Transform>().position.x+2 && position.y == selected.GetComponent<Transform>().position.y) || (position.x == selected.GetComponent<Transform>().position.x-2 && position.y == selected.GetComponent<Transform>().position.y) || (position.x == selected.GetComponent<Transform>().position.x+1 && position.y == selected.GetComponent<Transform>().position.y-1) || (position.x == selected.GetComponent<Transform>().position.x-1 && position.y == selected.GetComponent<Transform>().position.y-1) || (position.x == selected.GetComponent<Transform>().position.x-1 && position.y == selected.GetComponent<Transform>().position.y+1) || (position.x == selected.GetComponent<Transform>().position.x+1 && position.y == selected.GetComponent<Transform>().position.y+1)))
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if(Physics.Raycast(ray, out hit) && hit.transform.tag == "Enemy")
				{
					hit.transform.gameObject.GetComponent<Character>().CurrentHealth = hit.transform.gameObject.GetComponent<Character>().CurrentHealth - 1;
					chatText.GetComponent<textToChat>().processLine(selected.name + " dealt 1 damage to " + hit.transform.gameObject.name);
					selectAttack = false;
					endButton();
				}
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
		else if(selected.tag == "Enemy")
		{
			foreach(GameObject button in buttons)
			{
				button.SetActive(false);
			}
			float enemySquare = 10000f;
			Vector3 enemyPosition = new Vector3(0,0,0);
			foreach(GameObject c in characters)
			{
				if((Mathf.Abs(c.GetComponent<Transform>().position.x-selected.GetComponent<Transform>().position.x)+Mathf.Abs(c.GetComponent<Transform>().position.y-selected.GetComponent<Transform>().position.y)) < enemySquare)
				{
				  enemySquare = (Mathf.Abs(c.GetComponent<Transform>().position.x-selected.GetComponent<Transform>().position.x)+Mathf.Abs(c.GetComponent<Transform>().position.y-selected.GetComponent<Transform>().position.y));
				  enemyPosition = c.GetComponent<Transform>().position;
				}
			  }
			
			if(checkOccupancy(new Vector3(enemyPosition.x+1, enemyPosition.y, enemyPosition.z)) && tilemap.GetTile(tilemap.WorldToCell(new Vector3(enemyPosition.x+1, enemyPosition.y, 0))).name != "mountains")
			{
				enemyPosition = new Vector3(enemyPosition.x+1, enemyPosition.y, enemyPosition.z);
			}
			else if(checkOccupancy(new Vector3(enemyPosition.x, enemyPosition.y+1, enemyPosition.z)) && tilemap.GetTile(tilemap.WorldToCell(new Vector3(enemyPosition.x, enemyPosition.y+1, 0))).name != "mountains")
			{
				enemyPosition = new Vector3(enemyPosition.x, enemyPosition.y+1, enemyPosition.z);
			}
			else if(checkOccupancy(new Vector3(enemyPosition.x-1, enemyPosition.y, enemyPosition.z)) && tilemap.GetTile(tilemap.WorldToCell(new Vector3(enemyPosition.x-1, enemyPosition.y, 0))).name != "mountains")
			{
				enemyPosition = new Vector3(enemyPosition.x-1, enemyPosition.y, enemyPosition.z);
			}
			else if(checkOccupancy(new Vector3(enemyPosition.x, enemyPosition.y-1, enemyPosition.z)) && tilemap.GetTile(tilemap.WorldToCell(new Vector3(enemyPosition.x, enemyPosition.y-1, 0))).name != "mountains")
			{
				enemyPosition = new Vector3(enemyPosition.x, enemyPosition.y-1, enemyPosition.z);
			}
			//Make an else to go to second most if first is surrounded
			Debug.Log(enemyPosition.x);
			Debug.Log(enemyPosition.y);
			List<Point> p = stepTowardsPath(enemyPosition.x, enemyPosition.y);
			StartCoroutine(stepThroughPath(p));
			selected.GetComponent<Character>().reduceInitiative();
		}
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
				if(combatMembers[m].GetComponent<Character>().curInitiative > combatMembers[greatestPosition].GetComponent<Character>().curInitiative)
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

	
	public void attackButton()
	{
    if(!moving)
    {
		if(selected.GetComponent<Character>().currentActionPoints >= 2)
		{
			selectAttack = true;
			if(range == 1)
			{
				Vector3Int tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x+1, selected.GetComponent<Transform>().position.y, 0));
				TileBase tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x-1, selected.GetComponent<Transform>().position.y, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x, selected.GetComponent<Transform>().position.y+1, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x, selected.GetComponent<Transform>().position.y-1, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}


			}
			else if(range == 2)
			{
				Vector3Int tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x+1, selected.GetComponent<Transform>().position.y, 0));
				TileBase tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x-1, selected.GetComponent<Transform>().position.y, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x, selected.GetComponent<Transform>().position.y+1, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x, selected.GetComponent<Transform>().position.y-1, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x+2, selected.GetComponent<Transform>().position.y, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x-2, selected.GetComponent<Transform>().position.y, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x, selected.GetComponent<Transform>().position.y+2, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x, selected.GetComponent<Transform>().position.y-2, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x+1, selected.GetComponent<Transform>().position.y+1, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x+1, selected.GetComponent<Transform>().position.y-1, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x-1, selected.GetComponent<Transform>().position.y+1, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}
				tileLoc = tilemap.WorldToCell(new Vector3(selected.GetComponent<Transform>().position.x-1, selected.GetComponent<Transform>().position.y-1, 0));
				tempTile = tilemap.GetTile(tileLoc);
				if(tempTile.name == "grasslands")
				{
					tilemap.SetTile(tileLoc, grassLandsAttack);
				}

			}
    }
	}
	}

	public void skillButton()
	{
    if(!moving)
    {
		if(selected.GetComponent<Character>().currentActionPoints >= 2)
		{
			selectSkill = true;
		}
	}
	}

	public void returnButton()
	{
    if(!moving)
    {
    selected.GetComponent<Character>().currentActionPoints = prevPoints;
		selected.GetComponent<Transform>().position = prevPos;
		buttonPressed = true;
		selectAttack = false;
		resetMap();
  }
	}

	public void endButton()
	{
    if(!moving)
    {
		endTurn = true;
		selected.GetComponent<Character>().reduceActionPoints(selected.GetComponent<Character>().currentActionPoints);
		buttonPressed = true;
		selectAttack = false;
		resetMap();
  }
	}

	void resetMap()
	{

		int tilemapWidth = tilemap.size.x;
		int tilemapHeight = tilemap.size.y;
		for(int width = tilemap.origin.x; width < tilemapWidth; width++)
		{
			for(int height = tilemap.origin.y; height < tilemapHeight; height++)
			{

				Vector3Int replaceTile = new Vector3Int(width, height, 0);
				TileBase checkTile = tilemap.GetTile(replaceTile);
        if(checkTile != null)
        {
  				if(checkTile.name == "grassland_tile_attack")
  				{
  					tilemap.SetTile(replaceTile, grassLands);
  				}
  				else if(checkTile.name == "mountain_tile_attack")
  				{
  					tilemap.SetTile(replaceTile, mountain);
  				}
        }
			}
		}

	}

	class Point
	{
		public float X;
		public float Y;
		public Point(float x, float y)
		{
			X = x;
			Y = y;
		}
	}

	List<Point> stepTowardsPath(float x, float y)
	{
		List<Point> path = new List<Point>();
		int moves = selected.GetComponent<Character>().currentActionPoints;
		float movingX = selected.GetComponent<Transform>().position.x;
		float movingY = selected.GetComponent<Transform>().position.y;
		while(true)
		{

		  if(x > movingX && moves > 0)
				{
				if(tilemap.GetTile(tilemap.WorldToCell(new Vector3(movingX+1, movingY, 0))).name == "mountains")
				{
				  if(y > movingY)
				  {
					Point tempPoint = new Point(movingX, movingY+1);
							path.Add(tempPoint);
							movingY++;
							moves--;
				  }
				  else if(y < movingY)
				  {
					Point tempPoint = new Point(movingX, movingY-1);
							path.Add(tempPoint);
							movingY++;
							moves--;
				  }
				  else
				  {
					return path;
				  }
				}
				else
				{
						Point tempPoint = new Point(movingX+1, movingY);
						path.Add(tempPoint);
						movingX++;
						moves--;
				}
				}
		else if(x < movingX && moves > 0)
			{
        if(tilemap.GetTile(tilemap.WorldToCell(new Vector3(movingX-1, movingY, 0))).name == "mountains")
        {
          if(y > movingY)
          {
            Point tempPoint = new Point(movingX, movingY+1);
    				path.Add(tempPoint);
    				movingY++;
    				moves--;
          }
          else if(y < movingY)
          {
            Point tempPoint = new Point(movingX, movingY-1);
    				path.Add(tempPoint);
    				movingY++;
    				moves--;
          }
          else
          {
            return path;
          }
        }
        else
        {
  				Point tempPoint = new Point(movingX-1, movingY);
  				path.Add(tempPoint);
  				movingX--;
  				moves--;
        }
			}
			else if(y > movingY && moves > 0)
			{
        if(tilemap.GetTile(tilemap.WorldToCell(new Vector3(movingX, movingY+1, 0))).name == "mountains")
        {
          if(x > movingX)
          {
            Point tempPoint = new Point(movingX+1, movingY);
    				path.Add(tempPoint);
    				movingY++;
    				moves--;
          }
          else if(x < movingX)
          {
            Point tempPoint = new Point(movingX-1, movingY);
    				path.Add(tempPoint);
    				movingY++;
    				moves--;
          }
          else
          {
            return path;
          }
        }
        else
        {
  				Point tempPoint = new Point(movingX, movingY+1);
  				path.Add(tempPoint);
  				movingY++;
  				moves--;
        }
			}
			else if(y < movingY && moves > 0)
			{
        if(tilemap.GetTile(tilemap.WorldToCell(new Vector3(movingX, movingY-1, 0))).name == "mountains")
        {
          if(x > movingX)
          {
            Point tempPoint = new Point(movingX+1, movingY);
    				path.Add(tempPoint);
    				movingY++;
    				moves--;
          }
          else if(x < movingX)
          {
            Point tempPoint = new Point(movingX-1, movingY);
    				path.Add(tempPoint);
    				movingY++;
    				moves--;
          }
          else
          {
            return path;
          }
        }
        else
        {
  				Point tempPoint = new Point(movingX, movingY-1);
  				path.Add(tempPoint);
  				movingY--;
  				moves--;
        }
			}
			else if(moves == 0 || (movingX == x && movingY == y))
			{
				return path;
			}

		}
	}

	IEnumerator stepThroughPath(List<Point> path)
	{
    moving = true;
		foreach(Point p in path)
		{
			selected.GetComponent<Transform>().position = new Vector3(p.X, p.Y, 0f);
		  selected.GetComponent<Character>().reduceActionPoints(1);
		  if(cameraLock)
			{
			  Camera.main.transform.position = new Vector3(Mathf.Clamp(selected.GetComponent<Transform>().position.x, minX, maxX), Mathf.Clamp(selected.GetComponent<Transform>().position.y, minY, maxY), -10);
			  Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);
			}
			yield return new WaitForSeconds(.25f);
		}
    if(selected.tag == "Enemy")
    {
		if(selected.GetComponent<Character>().currentActionPoints >= 2 && !checkCharacterOccupancy(new Vector3(selected.transform.position.x+1, selected.transform.position.y, selected.transform.position.z)).Item1)
		{
			checkCharacterOccupancy(new Vector3(selected.transform.position.x+1, selected.transform.position.y, selected.transform.position.z)).Item2.GetComponent<Character>().CurrentHealth = checkCharacterOccupancy(new Vector3(selected.transform.position.x+1, selected.transform.position.y, selected.transform.position.z)).Item2.GetComponent<Character>().CurrentHealth - selected.GetComponent<Character>().Damage;
			chatText.GetComponent<textToChat>().processLine(selected.name + " dealt " + selected.GetComponent<Character>().Damage + " damage to " + checkCharacterOccupancy(new Vector3(selected.transform.position.x+1, selected.transform.position.y, selected.transform.position.z)).Item2.name);
		}
		else if(selected.GetComponent<Character>().currentActionPoints >= 2 && !checkCharacterOccupancy(new Vector3(selected.transform.position.x-1, selected.transform.position.y, selected.transform.position.z)).Item1)
		{
			checkCharacterOccupancy(new Vector3(selected.transform.position.x-1, selected.transform.position.y, selected.transform.position.z)).Item2.GetComponent<Character>().CurrentHealth = checkCharacterOccupancy(new Vector3(selected.transform.position.x-1, selected.transform.position.y, selected.transform.position.z)).Item2.GetComponent<Character>().CurrentHealth - selected.GetComponent<Character>().Damage;
			chatText.GetComponent<textToChat>().processLine(selected.name + " dealt " + selected.GetComponent<Character>().Damage + " damage to " + checkCharacterOccupancy(new Vector3(selected.transform.position.x-1, selected.transform.position.y, selected.transform.position.z)).Item2.name);
		}
		else if(selected.GetComponent<Character>().currentActionPoints >= 2 && !checkCharacterOccupancy(new Vector3(selected.transform.position.x, selected.transform.position.y+1, selected.transform.position.z)).Item1)
		{
			checkCharacterOccupancy(new Vector3(selected.transform.position.x, selected.transform.position.y+1, selected.transform.position.z)).Item2.GetComponent<Character>().CurrentHealth = checkCharacterOccupancy(new Vector3(selected.transform.position.x, selected.transform.position.y+1, selected.transform.position.z)).Item2.GetComponent<Character>().CurrentHealth - selected.GetComponent<Character>().Damage;
			chatText.GetComponent<textToChat>().processLine(selected.name + " dealt " + selected.GetComponent<Character>().Damage + " damage to " + checkCharacterOccupancy(new Vector3(selected.transform.position.x, selected.transform.position.y+1, selected.transform.position.z)).Item2.name);
		}
		else if(selected.GetComponent<Character>().currentActionPoints >= 2 && !checkCharacterOccupancy(new Vector3(selected.transform.position.x, selected.transform.position.y-1, selected.transform.position.z)).Item1)
		{
			checkCharacterOccupancy(new Vector3(selected.transform.position.x, selected.transform.position.y-1, selected.transform.position.z)).Item2.GetComponent<Character>().CurrentHealth = checkCharacterOccupancy(new Vector3(selected.transform.position.x, selected.transform.position.y-1, selected.transform.position.z)).Item2.GetComponent<Character>().CurrentHealth - selected.GetComponent<Character>().Damage;
			chatText.GetComponent<textToChat>().processLine(selected.name + " dealt " + selected.GetComponent<Character>().Damage + " damage to " + checkCharacterOccupancy(new Vector3(selected.transform.position.x, selected.transform.position.y-1, selected.transform.position.z)).Item2.name);
		}
		
		selected.GetComponent<Character>().currentActionPoints = selected.GetComponent<Character>().maxActionPoints;
    }
    moving = false;
	}

	bool checkOccupancy(Vector3 checkPosition)
	{
		foreach(GameObject s in combatMembers)
		{
			if(s.transform.position == checkPosition && s != selected)
			{
				return false;
			}
		}
		return true;
	}
	
	Tuple<bool,GameObject> checkCharacterOccupancy(Vector3 checkPosition)
	{
		foreach(GameObject s in characters)
		{
			if(s.transform.position == checkPosition)
			{
				return Tuple.Create(false, s);
			}
		}
		return Tuple.Create(true, selected);
	}

}
