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
        bool sizeCondition = ((size.x >= minimumSplitSize || size.y >= minimumSplitSize) && size.x > minimumRoomSize && size.y > minimumRoomSize);

        bool hallwayVerticalCondition = false;
        for (int x = minimumRoomSize; x < size.x - minimumRoomSize; x++)
        {
            bool possible = true;
            foreach (Vector2Int hallway in hallways)
            {
                for (int x2 = 0; x2 < 3; x2++)
                {
                    if (position.x + x + x2 == hallway.x) possible = false;
                }
            }
            if (possible) hallwayVerticalCondition = true;
        }

        bool hallwayHorizontalCondition = false;
        for (int y = minimumRoomSize; y < size.y - minimumRoomSize; y++)
        {
            bool possible = true;
            foreach (Vector2Int hallway in hallways)
            {
                for (int y2 = 0; y2 < 3; y2++)
                {
                    if (position.y + y + y2 == hallway.y) possible = false;
                }
            }
            if (possible) hallwayHorizontalCondition = true;
        }

        return (sizeCondition && hallwayVerticalCondition && hallwayHorizontalCondition);
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
        if(size.x > size.y)
        {
            // Split Vertical
            int splitIndex = FindVerticalSplit();
            if (splitIndex == 0) return new Room[] { this };
            int hallwayIndex = Random.Range(0, size.y - 2);
            Room sideA = new Room(position, new Vector2Int(splitIndex, size.y), tilemap);
            Room sideB = new Room(new Vector2Int(position.x+splitIndex+1, position.y), new Vector2Int(size.x-splitIndex-1, size.y), tilemap);
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
        else
        {
            // Split Horizontal
            int splitIndex = FindHorizontalSplit();
            if (splitIndex == 0) return new Room[] { this };
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
    }

    int FindVerticalSplit()
    {
        List<int> possibilites = Enumerable.Range(minimumRoomSize, size.x - minimumRoomSize).ToList();
        foreach (Vector2Int hallway in hallways)
        {
            for (int x = 0; x < 3; x++)
            {
                possibilites.Remove(hallway.x - position.x + x);
            }
        }

        int splitIndex = possibilites[Random.Range(0, possibilites.Count)];

        return splitIndex;
    }
    int FindHorizontalSplit()
    {
        List<int> possibilites = Enumerable.Range(minimumRoomSize, size.y - minimumRoomSize).ToList();
        foreach (Vector2Int hallway in hallways)
        {
            for (int y = 0; y < 3; y++)
            {
                possibilites.Remove(hallway.y - position.y + y);
            }
        }

        int splitIndex = possibilites[Random.Range(0, possibilites.Count)];

        return splitIndex;
    }
}
