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

    void OnSprintButtonClicked() => controller.UseAbility(PlayerActions.Primary);

    void OnLungeButtonClicked() => controller.BeginChoosingTarget(PlayerActions.Secondary);

    void OnSprintStart()
    {
        sprintButton.border.enabled = true;

        // TOOD(Matt): Refactor for Live cooldown fetching
        float remainingCooldown = controller.target.GetAbility(PlayerActions.Primary).RemainingCooldown;
        sprintButton.BeginCooldown(remainingCooldown);
    }

    void OnSprintEnd() => sprintButton.border.enabled = false;

    void OnLungeStart()
    {
        lungeButton.border.enabled = true;

        // TOOD(Matt): Refactor for Live cooldown fetching
        float remainingCooldown = controller.target.GetAbility(PlayerActions.Secondary).RemainingCooldown;
        lungeButton.BeginCooldown(remainingCooldown);
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
        controller.target.GetAbility(PlayerActions.Primary).eventUse += OnSprintStart;
        controller.target.GetAbility(PlayerActions.Primary).eventEndUse += OnSprintEnd;
        controller.target.GetAbility(PlayerActions.Secondary).eventUse += OnLungeStart;
        controller.target.GetAbility(PlayerActions.Secondary).eventEndUse += OnLungeEnd;
        InputManager.Get.eventInputDeviceChanged += RefreshHotkeySprites;
    }

    void UnsubscribeInputEvents()
    {
        controller.target.GetAbility(PlayerActions.Primary).eventUse -= OnSprintStart;
        controller.target.GetAbility(PlayerActions.Primary).eventEndUse -= OnSprintEnd;
        controller.target.GetAbility(PlayerActions.Secondary).eventUse -= OnLungeStart;
        controller.target.GetAbility(PlayerActions.Secondary).eventEndUse -= OnLungeEnd;
        InputManager.Get.eventInputDeviceChanged -= RefreshHotkeySprites;
    }
}
