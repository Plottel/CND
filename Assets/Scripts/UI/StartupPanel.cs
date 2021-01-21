using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Deft;
using Deft.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartupPanel : UIPanel
{
    Button testSceneContainer;
    TMP_Dropdown testSceneDropdown;

    protected override void OnAwake()
    {
        testSceneContainer = Find<Button>("TestSceneContainer");
        testSceneDropdown = testSceneContainer.Find<TMP_Dropdown>("TestSceneDropdown");

        testSceneContainer.onClick.AddListener(OnTestSceneButtonClicked);
    }

    protected override void OnStart()
    {
#if UNITY_EDITOR
        string quickStartScene = EditorPrefs.GetString("QuickStartScene");

        if (!string.IsNullOrEmpty(quickStartScene))
            LoadTestScene(quickStartScene);
#endif
    }

    void OnTestSceneButtonClicked()
    {
        string testScene = testSceneDropdown.captionText.text;

        if (testScene != "Choose Test Scene")
            LoadTestScene(testScene);
    }

    void LoadTestScene(string sceneName)
    {
        UIManager.Get.CloseAllPanels();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
