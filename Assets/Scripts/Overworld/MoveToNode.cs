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
        if(Equals(tile.name, "Node"))
        {
          pos.z = -10;
          Camera.main.transform.position = (pos);
        }
      }
    }
}
