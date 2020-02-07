using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RemoveFog : MonoBehaviour
{
    Vector3 prevCamera;
    Tilemap tilemap;
    TileBase tile;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = this.GetComponent<Tilemap>();
        prevCamera = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(prevCamera != Camera.main.transform.position)
        {
            for(float x = Camera.main.transform.position.x-2; x <= Camera.main.transform.position.x+2; x++)
            {
                for(float y = Camera.main.transform.position.y-2; y <= Camera.main.transform.position.y+2; y++)
                {
                    Vector3 pos = new Vector3(x, y, -1f);
                    Vector3Int tileLocation = tilemap.WorldToCell(pos);
                    tile = tilemap.GetTile(tileLocation);
                    if(tile != null)
                    {
                      tilemap.SetTile(tileLocation, null);
                    }
                }
            }


        }
        prevCamera = Camera.main.transform.position;
    }
}
