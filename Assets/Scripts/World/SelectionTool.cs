using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionTool : EditorTool
{
    public void OnHover(WorldEditor editor, WorldEditorTile tile)
    {
        //Debug.Log("Selection on hover" + tile.pos.ToString());
    }

    public void OnClick(WorldEditor editor, WorldEditorTile tile)
    {
        Debug.Log("Selection on click" + tile.pos.ToString());
    }

    public void Cancel()
    {

    }
}
