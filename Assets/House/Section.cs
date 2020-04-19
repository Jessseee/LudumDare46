using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Section
{
    Vector2Int position;
    int size;
    Tilemap tilemap;
    Dictionary<string, TileBase> tileset;
    List<Room> rooms;
    
    public Section(Vector2Int position, int size, Tilemap tilemap)
    {
        this.position = position;
        this.size = size;
        this.tilemap = tilemap;
        this.tileset = TilesetManager.RandomSet();
        this.rooms = new List<Room>();
        
        //Add outer walls
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                //tilemap.SetTile(new Vector3Int(position.x + x, position.y + y, 0), tileset["Floor"]);
                if(x==0 || x==size-1 || y==0 || y==size-1)
                {
                    //tilemap.SetTile(new Vector3Int(position.x + x, position.y + y, 0), tileset["Wall"]);
                }
            }
        }

        rooms.Add(new Room(new Vector2Int(position.x + 1, position.y + 1), new Vector2Int(size-2, size-2), tilemap));

        GenerateRooms();
        foreach (Room room in rooms)
        {
            room.Display();
        }
    }

    void GenerateRooms()
    {
        IEnumerable<Room> sortedRooms = rooms.OrderBy(room => -room.size.x * room.size.y);
        Room currentRoom = sortedRooms.First();
        if (currentRoom.Splitable())
        {
            Room[] newRooms = rooms[0].Split();
            rooms.Remove(currentRoom);
            rooms.AddRange(newRooms);
            GenerateRooms();
        }
    }
}
