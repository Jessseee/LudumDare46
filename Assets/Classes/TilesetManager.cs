using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilesetManager
{
    static Dictionary<string, Dictionary<string, TileBase>> tileSets;

    //Load all tilesets on creation
    static TilesetManager()
    {
        tileSets = new Dictionary<string, Dictionary<string, TileBase>>();
        TileBase[] tileAssets = Resources.LoadAll<TileBase>("Sets");
        foreach (TileBase tile in tileAssets)
        {
            string[] tileInfo = tile.name.Split('_');
            if(tileInfo[2] == "rules")
            {
                if (!tileSets.ContainsKey(tileInfo[0])) tileSets[tileInfo[0]] = new Dictionary<string, TileBase>();
                tileSets[tileInfo[0]].Add(tileInfo[1], tile);
            }
        }
    }

    public static string[] Sets()
    {
        return tileSets.Keys.ToArray();
    }

    public static Dictionary<string, TileBase> Set(string name)
    {
        return tileSets[name];
    }

    public static TileBase Tile(string set, string name)
    {
        return tileSets[set][name];
    }
}
