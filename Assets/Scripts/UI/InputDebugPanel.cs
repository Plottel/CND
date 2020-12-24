using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Deft.Input;
using Deft.UI;

public class InputDebugPanel : UIPanel
{
    private TextMeshProUGUI schemeText;
    private TextMeshProUGUI actionText;
    private TextMeshProUGUI controlText;
    private Button gameplayButton;
    private Button uiButton;

    // Add buttons on DebugPanel to swap between Gameplay / UI
    // Gucci!
    protected override void OnAwake()
    {
        schemeText = Find<TextMeshProUGUI>("Scheme");
        actionText = Find<TextMeshProUGUI>("Action");
        controlText = Find<TextMeshProUGUI>("Control");

        schemeText.text = "";
        actionText.text = "";
        controlText.text = "";

        gameplayButton = Find<Button>("GameplayButton");
        uiButton = Find<Button>("UIButton");

        gameplayButton.onClick.AddListener(OnGameplayClicked);
        uiButton.onClick.AddListener(OnUIClicked);
    }

    private void OnGameplayClicked() => InputManager.Get.SetActiveScheme(InputScheme.Gameplay);
    private void OnUIClicked() => InputManager.Get.SetActiveScheme(InputScheme.UI);

    void LateUpdate()
    {
        RefreshPanel();
    }

    private void RefreshPanel()
    {
        schemeText.text = InputManager.Get.activeScheme.ToString();

        var reader = InputManager.Get.GetActiveReader<PlayerInputReader>();
        var snapshot = InputManager.Get.inputSnapshot;

        if (reader.AnyControlPressed(out var control))
            controlText.text = control.name;

        if (snapshot.GetActuatedActions(out List<ActionSnapshot> actions))
        {
            string actionNames = "";
            foreach (var action in actions)
                actionNames += PlayerActions.GetName(action.actionID) + " ";

            actionText.text = actionNames;
        }
    }
}
