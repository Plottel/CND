using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementTool : EditorTool
{
    public Prop selectedProp;

    public PlacementTool(GameObject selectedPropObject)
    {
        selectedProp = selectedPropObject.GetComponent<Prop>();
    }

    public void OnHover(WorldEditor editor, WorldEditorTile tile)
    {
        Transform selectedTransform = selectedProp.transform;
        selectedTransform.position = tile.transform.position;

        SpriteRenderer sr = selectedProp.GetComponent<SpriteRenderer>();
        selectedTransform.position += new Vector3(
            (sr.sprite.bounds.size.x - 1) / 2,
            (sr.sprite.bounds.size.y - 1) / 2,
            0
        );

        for (int i = 0; i < selectedProp.size.x; i++)
        {
            for (int j = 0; j < selectedProp.size.y; j++)
            {
                var highlightColour = CheckTileValidity(editor.world, tile.pos.x + i, tile.pos.y + j)
                    ? WorldEditorTile.TileState.Valid
                    : WorldEditorTile.TileState.Invalid;

                WorldEditorTile editorTile = editor.GetTile(tile.pos.x + i, tile.pos.y + j);
                editorTile.nextState = highlightColour;
                
                //if (!editor.areTilesVisible)
            }
        }
    }

    public void OnClick(WorldEditor editor, WorldEditorTile tile)
    {
        bool canPlace = true;
        for (int i = 0; i < selectedProp.size.x; i++)
        {
            for (int j = 0; j < selectedProp.size.y; j++)
            {
                canPlace = canPlace && CheckTileValidity(editor.world, tile.pos.x + i, tile.pos.y + j);
            }
        }

        Debug.Log("Can place? " + canPlace);
        if (canPlace)
        {
            var sr = selectedProp.GetComponent<SpriteRenderer>();
            sr.sortingOrder = 2;
            sr.enabled = editor.arePropsVisible;
            editor.world.PutProp(selectedProp, tile.pos.x, tile.pos.y);
            editor.SelectionMode();
        }
    }

    private bool CheckTileValidity(World world, int x, int y)
        => world.GetProp<Prop>(x, y) == null;
}
