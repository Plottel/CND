using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using Deft;
using Deft.UI;
using Deft.Input;

// TODO(Matt): The targeting here should possibly be offloaded to DogController
public class DogAbilityPanel : UIPanel
{
    SpriteAtlas inputAtlas;
    [HideInInspector] public DogController controller;

    AbilityButton sprintButton;
    AbilityButton lungeButton;

    bool isChoosingLungeTarget;

    protected override void OnAwake()
    {
        inputAtlas = UIManager.Get.inputAtlas;

        sprintButton = Find<AbilityButton>("SprintButton");
        lungeButton = Find<AbilityButton>("LungeButton");

        sprintButton.onClick.AddListener(OnSprintButtonClicked);
        lungeButton.onClick.AddListener(OnLungeButtonClicked);
    }

    void OnSprintButtonClicked() => controller.ActivateSprint();

    void OnLungeButtonClicked() => controller.BeginChoosingTarget();

    void OnChooseTargetChanged(bool value) => lungeButton.border.enabled = value;

    void OnSprintStart()
    {
        sprintButton.border.enabled = true;
        sprintButton.BeginCooldown(controller.target.SprintCooldown);
    }

    void OnSprintEnd() => sprintButton.border.enabled = false;

    void OnLungeStart()
    {
        lungeButton.border.enabled = false;
        lungeButton.BeginCooldown(controller.target.LungeCooldown);
    }

    void OnLungeEnd() => lungeButton.border.enabled = false;

    void RefreshHotkeySprites(InputDeviceType deviceType)
    {
        var reader = InputManager.Get.GetActiveReader<PlayerInputReader>();

        string sprintControlID = reader.GetControlID(PlayerActions.Primary);
        string sprintThumbName = string.Concat("thumb-", sprintControlID).ToLower();

        sprintButton.hotkey.sprite = inputAtlas.GetSprite(sprintThumbName);

        string lungeControlID = reader.GetControlID(PlayerActions.Secondary);
        string lungeThumbName = string.Concat("thumb-", lungeControlID).ToLower();

        lungeButton.hotkey.sprite = inputAtlas.GetSprite(lungeThumbName);
    }

    protected override void OnVisibilityChanged(bool value)
    {
        if (value)
        {
            RefreshHotkeySprites(InputManager.Get.activeDeviceType);
            SubscribeInputEvents();
        }
        else
            UnsubscribeInputEvents();
    }

    void SubscribeInputEvents()
    {
        controller.target.eventSprintStart += OnSprintStart;
        controller.target.eventSprintEnd += OnSprintEnd;
        controller.target.eventLungeStart += OnLungeStart;
        controller.target.eventLungeEnd += OnLungeEnd;
        controller.eventIsChoosingTargetChanged += OnChooseTargetChanged;
        InputManager.Get.eventInputDeviceChanged += RefreshHotkeySprites;
    }

    void UnsubscribeInputEvents()
    {
        controller.target.eventSprintStart -= OnSprintStart;
        controller.target.eventSprintEnd -= OnSprintEnd;
        controller.target.eventLungeStart -= OnLungeStart;
        controller.target.eventLungeEnd -= OnLungeEnd;
        controller.eventIsChoosingTargetChanged -= OnChooseTargetChanged;
        InputManager.Get.eventInputDeviceChanged -= RefreshHotkeySprites;
    }
}
