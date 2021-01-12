using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EditorTool
{
    void OnHover(WorldEditor editor, WorldEditorTile tile);
    void OnClick(WorldEditor editor, WorldEditorTile tile);
}
