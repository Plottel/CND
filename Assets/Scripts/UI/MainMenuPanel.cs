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

public class MainMenuPanel : UIPanel
{
    TMP_Dropdown testSceneDropdown;
    Button testSceneButton;
    Button enterGameButton;

    protected override void OnAwake()
    {
        testSceneButton = Find<Button>("TestSceneButton");
        testSceneDropdown = testSceneButton.Find<TMP_Dropdown>("TestSceneDropdown");

        enterGameButton = Find<Button>("EnterGameButton");

        testSceneButton.onClick.AddListener(OnTestSceneButtonClicked);
        enterGameButton.onClick.AddListener(OnEnterGameButtonClicked);
    }

    protected override void OnStart()
    {
#if UNITY_EDITOR
        string quickStartScene = EditorPrefs.GetString("QuickStartScene");

        if (!string.IsNullOrEmpty(quickStartScene))
            LoadTestScene(quickStartScene);
#endif
    }

    void LoadTestScene(string sceneName)
    {
        GameManager.Get.SetState(GameState.TestScene);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    void OnTestSceneButtonClicked()
    {
        string testScene = testSceneDropdown.captionText.text;

        if (testScene != "Choose Test Scene")
            LoadTestScene(testScene);
    }

    void OnEnterGameButtonClicked()
    {
        SimulationManager.Get.EnterGame();
    }
}
