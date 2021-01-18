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

    [MenuItem("Deft/Quick Start Scenes/HotkeysTesting")]
    static void SetQuickStartHotkeysPanel()
        => QuickStarter.SetQuickStartScene("HotkeysTesting");

    [MenuItem("Deft/Quick Start Scenes/Clear")]
    static void ClearQuickStart()
        => QuickStarter.SetQuickStartScene("");
}