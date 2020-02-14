using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
      if(Input.GetMouseButtonDown(0))
      {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.x = (float)(System.Math.Floor(pos.x)+.5);
        pos.y = (float)(System.Math.Floor(pos.y)+.5);
        pos.z = 0;
        Vector3Int tileLocation = tilemap.WorldToCell(pos);//0,0 is the starting node location
        tile = tilemap.GetTile(tileLocation);
        if(Equals(tile.name, "Node") && (Next(pos)))
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
        
      }
      else
      {
        Debug.Log("Battle");
      }
    }
}
