﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class WorldEditor : MonoBehaviour
{
    private List<WorldEditorTile> tiles;

    public int worldWidth;
    public int worldHeight;
    public World world;
    public EditorTool tool;

    public bool arePropsVisible = true;
    public bool areTilesVisible = true;
    public bool isGridVisible = true;

    public GameObject tilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        world = new World(worldWidth, worldHeight, transform);

        tiles = new List<WorldEditorTile>();
        for (int j = 0; j < worldHeight; j++)
        {
            for (int i = 0; i < worldWidth; i++)
            {
                WorldEditorTile tile = Instantiate(tilePrefab, new Vector3(i - 4, j - 4, 0), Quaternion.identity, transform)
                    .GetComponent<WorldEditorTile>();
                tile.pos = new Vector2Int(i, j);
                //tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                tiles.Add(tile);
            }
        }

        EnableSelectionMode();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Keyboard.current.rightArrowKey.isPressed )
        {
            Camera.main.transform.Translate( new Vector3(0.1f, 0, 0) );
        }
        else if (Keyboard.current.leftArrowKey.isPressed)
        {
            Camera.main.transform.Translate( new Vector3(-0.1f, 0, 0) );
        }

        if (Keyboard.current.upArrowKey.isPressed)
        {
            Camera.main.transform.Translate( new Vector3(0, 0.1f, 0) );
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            Camera.main.transform.Translate( new Vector3(0, -0.1f, 0) );
        }

        if (Keyboard.current.minusKey.isPressed)
        {
            Camera.main.orthographicSize += 0.1f;
        }
        else if (Keyboard.current.equalsKey.isPressed)
        {
            Camera.main.orthographicSize -= 0.1f;
        }

        if (Keyboard.current.escapeKey.isPressed && tool != null)
        {
            tool.Cancel();
            tool = new SelectionTool();
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hitData = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hitData.transform != null)
        {
            var tile = hitData.transform.gameObject.GetComponent<WorldEditorTile>();
            //WorldEditorTile.TileState nextState = Mouse.current.leftButton.isPressed ? WorldEditorTile.TileState.Valid
            //    : Mouse.current.rightButton.isPressed ? WorldEditorTile.TileState.Invalid : WorldEditorTile.TileState.Active;
            //tile.nextState = nextState;
            tool.OnHover(this, tile);

            if (Mouse.current.leftButton.isPressed)
            {
                tool.OnClick(this, tile);
            }
        }
    }

    public WorldEditorTile GetTile(int idx)
        => tiles[idx];

    public WorldEditorTile GetTile(int x, int y)
        => tiles[y * worldWidth + x];

    public void Save()
    {
        world.Serialize(null);
    }

    public void Load()
    {
        world.Deserialize(null);
    }


    // Tooling methods
    public void EnablePlacementMode(GameObject propPrefab)
    {
        //if (tool != null) tool.Cancel();

        var prop = Instantiate(propPrefab);
        prop.GetComponent<SpriteRenderer>().sortingOrder = 3;
        tool = new PlacementTool(prop);
        tool.OnHover( this, GetTile(0) );
        Debug.Log("Placement mode");
    }

    public void EnableSelectionMode()
    {
        //if (tool != null) tool.Cancel();

        tool = new SelectionTool();
        Debug.Log("Selection mode");
    }

    public void EnableTileMode(Sprite sprite)
    {
        //if (tool != null) tool.Cancel();

        var tilePreview = new GameObject();
        var tileSr = tilePreview.AddComponent<SpriteRenderer>();
        tileSr.sortingOrder = 1;
        tileSr.sprite = sprite;
        tool.OnHover( this, GetTile(0) );
        tool = new TileTool(tilePreview);
    }


    // Editor Utilities
    public void TogglePropVisibility()
    {
        arePropsVisible = !arePropsVisible;
        foreach (Prop prop in world.propAnchors.Keys)
        {
            prop.GetComponent<SpriteRenderer>().enabled = arePropsVisible;
        }
    }
    public void ToggleTileVisibility()
    {
        areTilesVisible = !areTilesVisible;

        for (int i = 0; i < world.tiles.size.x; i++)
        {
            for (int j = 0; j < world.tiles.size.y; j++)
            {

            }
        }

    }

    public void ToggleGridVisibility()
    {
        isGridVisible = !isGridVisible;
        foreach (Transform cell in transform)
        {
            if (cell.name != "Grid")
                cell.GetComponent<SpriteRenderer>().enabled = isGridVisible;
        }
    }

    public void ResetCamera()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);
        Camera.main.orthographicSize = 5;
    }
}
