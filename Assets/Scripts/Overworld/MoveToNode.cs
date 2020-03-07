using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MoveToNode : MonoBehaviour
{
    Tilemap tilemap;
    TileBase tile;
    List<(float, float)> tupleList = new List<(float, float)>{};
    // Start is called before the first frame update
    void Start()
    {
      tilemap = this.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
      {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.x = (float)(System.Math.Floor(pos.x)+.5);
        pos.y = (float)(System.Math.Floor(pos.y)+.5);
        pos.z = 0;
        Vector3Int tileLocation = tilemap.WorldToCell(pos);//0,0 is the starting node location
        tile = tilemap.GetTile(tileLocation);
        if((Equals(tile.name, "Node1") || Equals(tile.name, "Node2") ||Equals(tile.name, "Node3") ||Equals(tile.name, "Node4") ||Equals(tile.name, "Node5") ||Equals(tile.name, "Node6") ||Equals(tile.name, "Node7") ||Equals(tile.name, "Node8") ||Equals(tile.name, "Node9") ||Equals(tile.name, "Node10") ||Equals(tile.name, "Node11") ||Equals(tile.name, "Node12")) && (Next(pos)))
        {
          pos.z = -10;
          Camera.main.transform.position = (pos);
          loadEvent();
        }
      }
    }

    bool Next(Vector3 pos)
    {
      (float,float) t = (pos.x, pos.y);
      bool move = (!(tupleList.Contains(t)) && ((Camera.main.transform.position.x+2 == pos.x && Camera.main.transform.position.y == pos.y ) || (Camera.main.transform.position.y+2 == pos.y && Camera.main.transform.position.x == pos.x ) || (Camera.main.transform.position.y-2 == pos.y && Camera.main.transform.position.x == pos.x )));
      if(move)
      {
          tupleList.Add(t);
      }
      return(move);
    }

    void loadEvent()
    {
      float r = UnityEngine.Random.Range(0.0f, 10.0f);
      if(r < 2.0f)
      {
        Debug.Log("Story Event");
      }
      else if(r < 4.0f && r > 2.0f)
      {
        Debug.Log("Town");
        SceneManager.LoadScene("Town_menu");
      }
      else
      {
        Debug.Log("Battle");
        SceneManager.LoadScene("Combat");
      }
    }
}
