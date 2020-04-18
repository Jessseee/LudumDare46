using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilesetManager
{
    static Dictionary<string, TileBase> tiles;

    //Load all tilesets on creation
    static TilesetManager()
    {
        tiles = new Dictionary<string, TileBase>();
        TileBase[] tileAssets = Resources.LoadAll<TileBase>("tilesets/test/tiles");
        foreach (TileBase tile in tileAssets)
        {
            tiles.Add(tile.name, tile);
        }
    }

    public static TileBase Tile(string name)
    {
        return tiles[name];
    }
}
