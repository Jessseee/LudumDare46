using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{
    static int[] NORTH = { 1, 0 };
    static int[] EAST = { 2, 1 };
    static int[] SOUTH = { 1, 2 };
    static int[] WEST = { 0, 1 };

    public Tilemap tilemap;

    Dictionary<string, TileBase> tileset;
    int size = 16;
    // Start is called before the first frame update
    void Start()
    {
        tileset = TilesetManager.RandomSet();
        GenerateRoom();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) AddDoor(NORTH);
        if (Input.GetKeyDown(KeyCode.RightArrow)) AddDoor(EAST);
        if (Input.GetKeyDown(KeyCode.DownArrow)) AddDoor(SOUTH);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) AddDoor(WEST);
    }

    void GenerateRoom()
    {
        tilemap.ClearAllTiles();
        for (int x = 1; x < size-1; x++)
        {
            for (int y = 2; y < size-1; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tileset["Floor"]);
            }
        }
    }

    void AddDoor(int[] side)
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                int xPos = side[0] * size / 2 + x - 1;
                int yPos = side[1] * size / 2 + y - 1;
                if(xPos < size && yPos < size) tilemap.SetTile(new Vector3Int(xPos, yPos, 0), tileset["Floor"]);
            }
        }
    }
}
