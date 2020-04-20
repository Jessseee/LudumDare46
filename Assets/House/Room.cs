using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room
{

    Vector2Int position;
    public Vector2Int size;
    Tilemap tilemap;
    Dictionary<string, TileBase> tileset;
    int minimumRoomSize = 5;
    int minimumSplitSize;

    List<Vector2Int> hallways;

    public Room(Vector2Int position, Vector2Int size, Tilemap tilemap)
    {
        this.position = position;
        this.size = size;
        this.tilemap = tilemap;
        this.tileset = TilesetManager.RandomSet();
        this.minimumSplitSize = minimumRoomSize * 2 + 1;
        this.hallways = new List<Vector2Int>();
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
        foreach (Vector2Int hallway in hallways)
        {
            if(hallway.x < position.x || hallway.x >= position.x + size.x)
            {
                for (int y = 0; y < 3; y++)
                {
                    tilemap.SetTile(new Vector3Int(hallway.x, hallway.y + y, 0), tileset["Floor"]);
                }
            }
            else
            {
                for (int x = 0; x < 3; x++)
                {
                    tilemap.SetTile(new Vector3Int(hallway.x + x, hallway.y, 0), tileset["Floor"]);
                }
            }
        }
    }

    public bool Splitable()
    {
        if (size.x > minimumRoomSize && size.y > minimumRoomSize)
        {
            if (size.x >= minimumSplitSize)
            {
                if (FindSplits(0).Length > 0) return true;
            }
            if (size.y >= minimumSplitSize) {
                if (FindSplits(1).Length > 0) return true;
            }
        }
        return false;
    }

    public void AddHallway(Vector2Int hallway)
    {
        hallways.Add(hallway);
    }

    public void AddRelativeHallway(Vector2Int hallway)
    {
        hallways.Add(hallway + position);
    }

    public Room[] Split()
    {
        if((size.x > size.y || FindSplits(1).Length == 0) && FindSplits(0).Length > 0)
        {
            return SplitVertical();
        }
        else if (FindSplits(1).Length > 0)
        {
            return SplitHorizontal();
        }
        return null;
    }

    Room[] SplitVertical()
    {
        // Split Vertical
        int[] splits = FindSplits(0);
        LogSplits(splits);
        int splitIndex = splits[Random.Range(0, splits.Length)];
        if (splitIndex < minimumRoomSize || splitIndex > size.x - minimumRoomSize) return new Room[] { this };
        int hallwayIndex = Random.Range(0, size.y - 2);
        Room sideA = new Room(position, new Vector2Int(splitIndex, size.y), tilemap);
        Room sideB = new Room(new Vector2Int(position.x + splitIndex + 1, position.y), new Vector2Int(size.x - splitIndex - 1, size.y), tilemap);
        sideA.AddRelativeHallway(new Vector2Int(splitIndex, hallwayIndex));
        sideB.AddRelativeHallway(new Vector2Int(-1, hallwayIndex));
        foreach (Vector2Int hallway in hallways)
        {
            if (hallway.x < splitIndex)
            {
                sideA.AddHallway(hallway);
            }
            else
            {
                sideB.AddHallway(hallway);
            }
        }
        return new Room[] { sideA, sideB };
    }

    Room[] SplitHorizontal()
    {
        // Split Horizontal
        int[] splits = FindSplits(1);
        LogSplits(splits);
        int splitIndex = splits[Random.Range(0, splits.Length)];
        if (splitIndex < minimumRoomSize || splitIndex > size.y - minimumRoomSize) return new Room[] { this };
        int hallwayIndex = Random.Range(0, size.x - 2);
        Room sideA = new Room(position, new Vector2Int(size.x, splitIndex), tilemap);
        Room sideB = new Room(new Vector2Int(position.x, position.y + splitIndex + 1), new Vector2Int(size.x, size.y - splitIndex - 1), tilemap);
        sideA.AddRelativeHallway(new Vector2Int(hallwayIndex, splitIndex));
        sideB.AddRelativeHallway(new Vector2Int(hallwayIndex, -1));
        foreach (Vector2Int hallway in hallways)
        {
            if (hallway.y < splitIndex)
            {
                sideA.AddHallway(hallway);
            }
            else
            {
                sideB.AddHallway(hallway);
            }
        }
        return new Room[] { sideA, sideB };
    }

    int[] FindSplits(int direction)
    {
        List<int> possibilites = Enumerable.Range(minimumRoomSize, (direction == 0 ? size.x : size.y) - minimumRoomSize * 2).ToList();
        foreach (Vector2Int hallway in hallways)
        {
            for (int i = 0; i < 3; i++)
            {
                if (direction == 0) possibilites.Remove(hallway.x - position.x + i);
                else possibilites.Remove(hallway.y - position.y + i);
            }
        }

        return possibilites.ToArray();
    }

    void LogSplits(int[] splits)
    {
        string message = "Splits: ";
        foreach (int split in splits) message += split + ", ";
        Debug.Log(message);
    }
}
