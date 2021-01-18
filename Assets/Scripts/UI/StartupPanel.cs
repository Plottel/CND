using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Deft;
using Deft.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartupPanel : UIPanel
{
    protected override void OnStart()
    {
#if UNITY_EDITOR
        string quickStartScene = EditorPrefs.GetString("QuickStartScene");

        if (!string.IsNullOrEmpty(quickStartScene))
        {
            UIManager.Get.CloseAllPanels();
            SceneManager.LoadScene(quickStartScene, LoadSceneMode.Additive);
        }
#endif
    }
}
