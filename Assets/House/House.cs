using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class House : MonoBehaviour
{
    public Tilemap tilemap;
    int sectionSize = 32;

    Dictionary<int, Dictionary<int, Section>> sections;
    // Start is called before the first frame update
    void Start()
    {
        sections = new Dictionary<int, Dictionary<int, Section>>();

        // Create initial rooms
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if(x!=0 || y!=0)
                {
                    Debug.Log("Add Section {" + x + "," + y + "}!");
                    AddSection(new Vector2Int(x, y));
                }
            }
        }

        //Set room neighbours

    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<int, Dictionary<int, Section>> sectionCol in sections) {
            foreach (KeyValuePair<int, Section> section in sectionCol.Value) section.Value.Update();
        }
    }

    // Add a room
    void AddSection(Vector2Int pos)
    {
        if (!sections.ContainsKey(pos.x)) sections[pos.x] = new Dictionary<int, Section>();
        sections[pos.x][pos.y] = new Section(pos * sectionSize, sectionSize, tilemap);
    }
}
