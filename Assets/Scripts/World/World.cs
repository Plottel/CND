using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World
{
    public Prop[,] props;
    public Dictionary<Prop, Vector2Int> propAnchors = new Dictionary<Prop, Vector2Int>();

    public World(int w, int h)
    {
        props = new Prop[w, h];
    }

    public void PutProp(Prop prop, int x, int y)
    {
        for (int i = 0; i < prop.size.x; i++)
        {
            for (int j = 0; j < prop.size.y; j++)
            {
                var aProp = props[x + i, y + j];

                props[x + i, y + j] = prop;
            }
        }

        propAnchors.Add(prop, new Vector2Int(x, y));
    }

    public Prop GetProp<T>(Vector2Int pos)
    {
        return props[pos.x, pos.y];
    }

    public Prop GetProp<T>(int x, int y)
    {
        return props[x, y];
    }

    public List<Vector2Int> GetProp(Prop prop)
    {
        return null;
    }

    public void Load(string path)
    {

    }

    public void Save(string path)
    {

    }
}
