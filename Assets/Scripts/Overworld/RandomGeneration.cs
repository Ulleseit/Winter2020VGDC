using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomGeneration : MonoBehaviour
{
    Tilemap tilemap;
    TileBase tile;
    // Start is called before the first frame update
    void Start()
    {
        //tilemap = this.GetComponent<Tilemap>();
        //Vector3Int tileLocation = tilemap.WorldToCell(new Vector3(.5f, .5f, 0f));//0,0 is the starting node location
        //tile = tilemap.GetTile(tileLocation);
        //createNode(tile);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void createNode(TileBase Node)
    {
        //Debug.Log(Node.GetTileData());
    }
}
