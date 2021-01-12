using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Deft.DevOps;

public static class QuickStartMenuItems
{
    [MenuItem("Deft/Toggle Play _F5")]
    public static void TogglePlayMode()
    => EditorApplication.ExecuteMenuItem("Edit/Play");

    [MenuItem("Deft/Quick Start - Hotkeys Panel")]
    static void SetQuickStartHotkeysPanel()
    {
        QuickStarter.SetQuickStartRoutine("HotkeysPanelRoutine");
    }

    [MenuItem("Deft/Quick Start - Clear")]
    static void ClearQuickStart()
    {
        QuickStarter.SetQuickStartRoutine("");
    }
}
