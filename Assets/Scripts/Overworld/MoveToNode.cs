using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveToNode : MonoBehaviour
{
    Tilemap tilemap;
    TileBase tile;
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
        Debug.Log(pos.x);
        Debug.Log(Camera.main.transform.position.x+2);
        Debug.Log(pos.y);
        Debug.Log(Camera.main.transform.position.y+2);
        if(Equals(tile.name, "Node") && ((Camera.main.transform.position.x+2 == pos.x && Camera.main.transform.position.y == pos.y )|| (Camera.main.transform.position.y+2 == pos.y && Camera.main.transform.position.x == pos.x )))
        {
          pos.z = -10;
          Camera.main.transform.position = (pos);
        }
      }
    }
}
