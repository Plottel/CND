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

        for (int row = 0; row < selectedProp.size.x; row++)
        {
            for (int col = 0; col < selectedProp.size.y; col++)
            {
                var highlightColour = CheckTileValidity(editor.world, tile.pos.x + row, tile.pos.y + col)
                    ? WorldEditorTile.TileState.Valid
                    : WorldEditorTile.TileState.Invalid;

                WorldEditorTile editorTile = editor.GetTile(tile.pos.x + row, tile.pos.y + col);
                editorTile.nextState = highlightColour;
                
                //if (!editor.areTilesVisible)
            }
        }
    }

    public void OnClick(WorldEditor editor, WorldEditorTile tile)
    {
        bool canPlace = true;
        for (int row = 0; row < selectedProp.size.x; row++)
        {
            for (int col = 0; col < selectedProp.size.y; col++)
            {
                canPlace = canPlace && CheckTileValidity(editor.world, tile.pos.x + row, tile.pos.y + col);
            }
        }

        if (canPlace)
        {
            var sr = selectedProp.GetComponent<SpriteRenderer>();
            sr.sortingOrder = 2;
            sr.enabled = editor.arePropsVisible;
            editor.world.PutProp(selectedProp, tile.pos.x, tile.pos.y);
            editor.EnableSelectionMode();
        }
    }

    public void Cancel()
    {
        GameObject.Destroy(selectedProp.gameObject);
    }

    private bool CheckTileValidity(World world, int x, int y)
        => world.GetProp(x, y) == null;
}
