using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTool : EditorTool
{
    public GameObject selectedTile;

    public TileTool(GameObject selectedTileObject)
    {
        selectedTile = selectedTileObject;
    }

    public void OnHover(WorldEditor editor, WorldEditorTile tile)
    {
        Transform selectedTransform = selectedTile.transform;
        selectedTransform.position = tile.transform.position;

        SpriteRenderer sr = selectedTile.GetComponent<SpriteRenderer>();
        selectedTransform.position += new Vector3(
            (sr.sprite.bounds.size.x - 1) / 2,
            (sr.sprite.bounds.size.y - 1) / 2,
            0
        );
    }

    public void OnClick(WorldEditor editor, WorldEditorTile tile)
    {
        var sr = selectedTile.GetComponent<SpriteRenderer>();
        if (editor.world.HasTileAt(tile.pos)) editor.world.RemoveTile(tile.pos);
        editor.world.PutTile(sr.sprite, tile.pos);
        editor.EnableSelectionMode();
    }

    public void Cancel()
    {

    }
}
