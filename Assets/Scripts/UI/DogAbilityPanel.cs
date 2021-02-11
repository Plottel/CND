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

    void OnLungeButtonClicked()
    {
        if (controller.CanActivateLunge())
        {
            isChoosingLungeTarget = true;
            lungeButton.border.enabled = true;
        }
    }

    void OnDogSprintStart()
    {
        sprintButton.border.enabled = true;
        sprintButton.BeginCooldown(controller.target.SprintCooldown);
    }

    void OnDogSprintEnd() => sprintButton.border.enabled = false;

    void OnMousePressed(int buttonID, Vector2 mousePos)
    {
        if (buttonID == 0)
        {
            if (isChoosingLungeTarget)
            {
                if (controller.ActivateLunge(mousePos))
                {
                    isChoosingLungeTarget = false;
                    lungeButton.border.enabled = false;
                    lungeButton.BeginCooldown(controller.target.LungeCooldown);
                }
            }
        }
    }

    void RefreshHotkeySprites(InputDeviceType deviceType)
    {
        var reader = InputManager.Get.GetActiveReader<PlayerInputReader>();

        string sprintControlID = reader.GetControlID(PlayerActions.Primary);
        string thumbName = string.Concat("thumb-", sprintControlID).ToLower();

        sprintButton.hotkey.sprite = inputAtlas.GetSprite(thumbName);
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
        controller.target.eventSprintStart += OnDogSprintStart;
        controller.target.eventSprintEnd += OnDogSprintEnd;
        InputManager.Get.eventMousePressed += OnMousePressed;
        InputManager.Get.eventInputDeviceChanged += RefreshHotkeySprites;
    }

    void UnsubscribeInputEvents()
    {
        controller.target.eventSprintStart -= OnDogSprintStart;
        controller.target.eventSprintEnd -= OnDogSprintEnd;
        InputManager.Get.eventMousePressed -= OnMousePressed;
        InputManager.Get.eventInputDeviceChanged -= RefreshHotkeySprites;
    }
}
