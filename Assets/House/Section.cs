using System;
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
        Debug.Log("Rooms: " + rooms.Count);
        Display();
    }

    void GenerateRooms()
    {
        List<Room> roomsToDelete = new List<Room>();
        List<Room> roomsToAdd = new List<Room>();
        foreach(Room room in rooms)
        {
            if (room.Splitable())
            {
                Room[] newRooms = room.Split();
                roomsToDelete.Add(room);
                foreach (Room newRoom in newRooms) roomsToAdd.Add(newRoom);
            }
        }
        foreach (Room room in roomsToDelete) rooms.Remove(room);
        foreach (Room room in roomsToAdd) rooms.Add(room);
        if (roomsToAdd.Count > 0) GenerateRooms();
    }

    void Display()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y =0; y < size; y++)
            {
                tilemap.SetTile(new Vector3Int(position.x + x, position.y + y, 0), null);
            }
        }

        foreach (Room room in rooms) room.Display();
    }
}

static class Extensions
{
    public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
    {
        return listToClone.Select(item => (T)item.Clone()).ToList();
    }
}
