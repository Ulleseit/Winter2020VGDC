using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterControl : MonoBehaviour
{
    GameObject[] characters;
    GameObject selected = null;
    float clickRate = .01f;
    float nextClick = 0f;
    void Start()
    {
    }
    void Update()
    {
      characters = GameObject.FindGameObjectsWithTag("Character");//Set up and update array of GameObjects
      Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      position.z = 0;
      position.x = (float)(System.Math.Floor(position.x)+.5);
      position.y = (float)(System.Math.Floor(position.y)+.5);//Make mouse only count at center of each square, instead of true position
      if(Input.GetMouseButtonDown(0) && selected == null && Time.time > nextClick)//Check if Player is clicking without having a character selected
      {
        nextClick = Time.time + clickRate;
        for(int i = 0; i < characters.Length; i++)
        {
          if(characters[i].GetComponent<Transform>().position.x == position.x && characters[i].GetComponent<Transform>().position.y == position.y)//If any character is in the square clicked, select character
          {
            selected = characters[i];//Save selected character for later use
          }
        }
      }
      if(Input.GetMouseButtonDown(0) && selected != null && Time.time > nextClick)//Check if Player is clicking with a selected character
      {
        bool matching = false;//Initialize check
        nextClick = Time.time + clickRate;
        for(int j = 0; j < characters.Length; j++)
        {
          if(characters[j].GetComponent<Transform>().position.x == position.x && characters[j].GetComponent<Transform>().position.y == position.y)
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
        }
        selected = null;//Deselect GameObject
      }
    }
}
