using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World
{
    public Prop[,] props;
    public Dictionary<Prop, Vector2Int> propAnchors;
    public Tilemap tiles;

    public World(int w, int h, Transform parent)
    {
        props = new Prop[w, h];
        propAnchors = new Dictionary<Prop, Vector2Int>();
        var grid = new GameObject("Grid").AddComponent<Grid>();
        grid.transform.SetParent(parent);
        tiles = new GameObject("TileMap").AddComponent<Tilemap>();
        tiles.transform.SetParent(grid.transform);
    }


    // PROP
    public Prop GetProp(int x, int y)
    {
        return props[x, y];
    }

    public Prop GetProp(Vector2Int pos)
    {
        return props[pos.x, pos.y];
    }

    public bool HasPropAt(int x, int y)
    {
        return props[x, y] != null;
    }

    public bool HasPropAt(Vector2Int pos)
    {
        return props[pos.x, pos.y] != null;
    }

    public List<Vector2Int> GetPropPos(Prop prop)
    {
        if ( !propAnchors.ContainsKey(prop) ) return null;
        return new List<Vector2Int>() { propAnchors[prop] };
    }

    public bool HasProp(Prop prop)
    {
        return propAnchors.ContainsKey(prop);
    }

    public void RemoveProp(Prop prop)
    {
        foreach ( Vector2Int pos in GetPropPos(prop) )
        {
            props[pos.x, pos.y] = null;
        }

        propAnchors.Remove(prop);
    }

    public void RemoveProp(int x, int y)
    {
        var prop = props[x, y];
        RemoveProp(prop);
    }

    public void RemoveProp(Vector2Int pos)
    {
        var prop = props[pos.x, pos.y];
        RemoveProp(prop);
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


    // TILE
    public TileBase GetTile(int x, int y)
    {
        return tiles.GetTile( new Vector3Int(x, y, 0) );
    }

    public TileBase GetTile(Vector2Int pos)
    {
        return GetTile(pos.x, pos.y);
    }

    public bool HasTileAt(Vector2Int pos)
    {
        return true;
    }

    public bool HasTileAt(int x, int y)
    {
        return true;
    }

    public void PutTile(Sprite sprite, int x, int y)
    {
        var tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = sprite;
        tiles.SetTile(new Vector3Int(x, y, 0), tile);
    }

    public void PutTile(Sprite sprite, Vector2Int pos)
    {
        PutTile(sprite, pos.x, pos.y);
    }

    public void RemoveTile(int x, int y)
    {
        tiles.SetTile(new Vector3Int(x, y, 0), null);
    }

    public void RemoveTile(Vector2Int pos)
    {
        RemoveTile(pos.x, pos.y);
    }


    // SERIALIZING
    public void Deserialize(string path)
    {

    }

    public void Serialize(string path)
    {
        Debug.Log("SAVING");
        Debug.Log("PROPS:");
        for (int i = 0; i < props.GetLength(0); i++ )
        {
            for (int j = 0; j < props.GetLength(1); j++)
            {
                var prop = props[i, j];
                if (prop != null) Debug.Log(i + "," + j + ": " + prop.gameObject.name);
            }
        }

        Debug.Log("TILES");

        for (int i = 0; i < tiles.size.x; i++)
        {
            for (int j = 0; j < tiles.size.y; j++)
            {
                var tile = tiles.GetTile<Tile>(new Vector3Int(i, j, 0));
                if (tile != null) Debug.Log(i + "," + j + ": " + tile.sprite.name);
            }
        }
    }
}
