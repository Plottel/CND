using UnityEngine;
using UnityEditor;

namespace Deft.DevOps
{
    public static class MenuItems
    {
        [MenuItem("Deft/Toggle Play _F5")]
        public static void TogglePlayMode()
            => EditorApplication.ExecuteMenuItem("Edit/Play");
    }
}

