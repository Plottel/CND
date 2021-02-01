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
    Button settingsButton;
    Button testButton;

    protected override void OnAwake()
    {
        testSceneButton = Find<Button>("TestSceneButton");
        testSceneDropdown = testSceneButton.Find<TMP_Dropdown>("TestSceneDropdown");

        enterGameButton = Find<Button>("EnterGameButton");
        settingsButton = Find<Button>("SettingsButton");

        testSceneButton.onClick.AddListener(OnTestSceneButtonClicked);
        enterGameButton.onClick.AddListener(OnEnterGameButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);

        testButton = Find<Button>("TestButton");
        testButton.onClick.AddListener(() => Debug.Log("Test Click"));

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
        DeftSimulationManager.Get.EnterGame();
    }

    void OnSettingsButtonClicked()
    {
        UIManager.Get.Hide<MainMenuPanel>();
        UIManager.Get.Show<HotkeysPanel>();
    }
}
