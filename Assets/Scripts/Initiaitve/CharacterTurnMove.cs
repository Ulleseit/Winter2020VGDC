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
	bool selectAttack = false;
	bool selectSkill = false;
    bool cameraLock = true;
	int range = 1;
	public TileBase grassLandsAttack;
	public TileBase mountainAttack;
	public TileBase grassLands;
	public TileBase mountain;
    void Start()
    {
		characters = GameObject.FindGameObjectsWithTag("Character");//Set up and update array of GameObjects that are player controlled
		enemies = GameObject.FindGameObjectsWithTag("Enemy");//Set up and update array of GameObjects that are AI controlled
		tilemap = this.GetComponent<Tilemap>();
		
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
						/*
						selected.GetComponent<Character>().reduceActionPoints((int)(Math.Abs(selected.GetComponent<Transform>().position.x - position.x) + Math.Abs(selected.GetComponent<Transform>().position.y - position.y)));
						selected.GetComponent<MoveCharacter>().move(position.x, position.y);//Move GameObject to selected space
						*/
						Debug.Log("X: " + position.x + "Y: " + position.y);
						List<Point> p = stepTowardsPath(position.x, position.y);
						foreach(Point point in p)
						{
							Debug.Log("X:" + point.X + " Y:" + point.Y);
						}
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
					hit.transform.gameObject.GetComponent<Character>().currentHealth = hit.transform.gameObject.GetComponent<Character>().currentHealth - 1;
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
					hit.transform.gameObject.GetComponent<Character>().currentHealth = hit.transform.gameObject.GetComponent<Character>().currentHealth - 1;
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
	
	public void attackButton()
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
	
	public void skillButton()
	{
		if(selected.GetComponent<Character>().currentActionPoints >= 2)
		{
			selectSkill = true;
		}
	}

	public void returnButton()
	{
		selected.GetComponent<Transform>().position = prevPos;
		buttonPressed = true;
		selectAttack = false;
		resetMap();
	}

	public void endButton()
	{
		endTurn = true;
		selected.GetComponent<Character>().reduceActionPoints(selected.GetComponent<Character>().currentActionPoints);
		buttonPressed = true;
		selectAttack = false;
		resetMap();
	}
	
	void resetMap()
	{
		int tilemapWidth = tilemap.size.x;
		int tilemapHeight = tilemap.size.y;
		for(int width = -1; width < tilemapWidth-2; width++)
		{
			for(int height = -1; height < tilemapHeight-2; height++)
			{
				
				Vector3Int replaceTile = new Vector3Int(width, height, 0);
				TileBase checkTile = tilemap.GetTile(replaceTile);
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
				Point tempPoint = new Point(movingX+1, movingY);
				path.Add(tempPoint);
				movingX++;
				moves--;
			}
			else if(x < movingX && moves > 0)
			{
				Point tempPoint = new Point(movingX-1, movingY);
				path.Add(tempPoint);
				movingX--;
				moves--;
			}
			else if(y > movingY && moves > 0)
			{
				Point tempPoint = new Point(movingX, movingY+1);
				path.Add(tempPoint);
				movingY++;
				moves--;
			}
			else if(y < movingY && moves > 0)
			{
				Point tempPoint = new Point(movingX, movingY-1);
				path.Add(tempPoint);
				movingY--;
				moves--;
			}
			else if(moves == 0 || (movingX == x && movingY == y))
			{
				return path;
			}
			
		}
	}
	/*
	void stepThroughPath(List<Point> path)
	{
		foreach(Point p in path)
		{
			selected.GetComponent<Transform>().position = new Vector3(Mathf.Lerp((selected.GetComponent<Transform>().position.x, p.X, 1f), (selected.GetComponent<Transform>().position.y, 
		}
	}
	*/
}
