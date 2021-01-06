using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.EventSystems;
using TMPro;
using Deft.Input;
using Deft.UI;


public class InputDebugPanel : UIPanel
{
    public TextMeshProUGUI schemeText;
    public TextMeshProUGUI actionText;
    public TextMeshProUGUI controlText;
    public TextMeshProUGUI modalKeyText;
    public TextMeshProUGUI modalControlText;

    public Button gameplayButton;
    public Button uiButton;
    public Button keyModalButton;

    protected override void OnAwake()
    {
        schemeText = Find<TextMeshProUGUI>("Scheme");
        actionText = Find<TextMeshProUGUI>("Action");
        controlText = Find<TextMeshProUGUI>("Control");
        modalKeyText = Find<TextMeshProUGUI>("ModalKey");
        modalControlText = Find<TextMeshProUGUI>("ModalControl");

        schemeText.text = "";
        actionText.text = "";
        controlText.text = "";
        modalKeyText.text = "";
        modalControlText.text = "";

        gameplayButton = Find<Button>("GameplayButton");
        uiButton = Find<Button>("UIButton");
        keyModalButton = Find<Button>("KeyModalButton");

        gameplayButton.onClick.AddListener(OnGameplayClicked);
        uiButton.onClick.AddListener(OnUIClicked);
        keyModalButton.onClick.AddListener(OnKeyClicked);
    }

    private void OnGameplayClicked()
    {
        InputManager.Get.SetActiveScheme(InputScheme.Gameplay);
    }

    private void OnUIClicked()
    {
        InputManager.Get.SetActiveScheme(InputScheme.UI);
    }

    private void OnKeyClicked()
    {
        UIManager.Get.PushModal<AnyKeyModalPanel, string>(OnAnyKeyModalPopped);
    }

    void OnAnyKeyModalPopped(string value)
    {
        modalKeyText.text = value;

        var reader = InputManager.Get.GetActiveReader<PlayerInputReader>();
        var control = reader.Find<ButtonControl>(value);

        if (control == null)
            modalControlText.text = "Null Control";
        else
        {
            modalControlText.text = "Path: \n" + control.path + "\n\n"
                + " Name: \n" + control.name + "\n\n"
                + "Display Name: \n" + control.displayName + "\n\n"
                + "Parent: \n" + control.parent + "\n\n"
                + "Parent Name: \n" + control.parent.name + "\n";
        }            

        // Need to track the Action being bound through InputDebugPanel
        // Time to redesign panel to be better... now we're making the HotKeysPanel
        // Attempt to Bind action
        if (control != null)
        {
            // How to get PlayerAction ID?
            // How to get Direction ID if applicable?
        }

    }

    private void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
            InputManager.Get.SetActiveScheme(InputScheme.Gameplay);
        else if (Keyboard.current.uKey.wasPressedThisFrame)
            InputManager.Get.SetActiveScheme(InputScheme.UI);
    }

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
