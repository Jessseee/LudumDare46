using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room
{

    Vector2Int position;
    public Vector2Int size;
    Tilemap tilemap;
    Dictionary<string, TileBase> tileset;
    int minimumRoomSize = 3;
    int minimumSplitSize = 7;

    public Room(Vector2Int position, Vector2Int size, Tilemap tilemap)
    {
        this.position = position;
        this.size = size;
        this.tilemap = tilemap;
        this.tileset = TilesetManager.RandomSet();
    }

    public void Display()
    {
        for(int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                tilemap.SetTile(new Vector3Int(position.x + x, position.y + y, 0), tileset["Floor"]);
            }
        }
    }

    public bool Splitable()
    {
        return ((size.x >= minimumSplitSize || size.y >= minimumSplitSize) && size.x > minimumRoomSize && size.y > minimumRoomSize);
    }

    public Room[] Split()
    {
        if(size.x > size.y)
        {
            // Split Vertical
            int splitIndex = Random.Range(minimumRoomSize, size.x - minimumRoomSize);
            int hallwayIndex = Random.Range(0, size.y - 1);
            Room sideA = new Room(position, new Vector2Int(splitIndex, size.y), tilemap);
            Room sideB = new Room(new Vector2Int(position.x+splitIndex+1, position.y), new Vector2Int(size.x-splitIndex-1, size.y), tilemap);
            Room hallway = new Room(new Vector2Int(position.x+splitIndex, position.y+hallwayIndex), new Vector2Int(1, 3), tilemap);
            return new Room[] { sideA, sideB };
        } else
        {
            // Split Horizontal
            int splitIndex = Random.Range(minimumRoomSize, size.y - minimumRoomSize);
            int hallwayIndex = Random.Range(0, size.x - 1);
            Room sideA = new Room(position, new Vector2Int(size.x, splitIndex), tilemap);
            Room sideB = new Room(new Vector2Int(position.x, position.y + splitIndex + 1), new Vector2Int(size.x, size.y - splitIndex - 1), tilemap);
            Room hallway = new Room(new Vector2Int(position.x + hallwayIndex, position.y + splitIndex), new Vector2Int(3, 1), tilemap);
            return new Room[] { sideA, sideB };
        }
    }
}
