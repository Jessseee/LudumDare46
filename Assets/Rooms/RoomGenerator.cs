﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public int size = 16;
    // Start is called before the first frame update
    void Start()
    {
        tilemap.ClearAllTiles();
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), TilesetManager.Tile("Floor"));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
