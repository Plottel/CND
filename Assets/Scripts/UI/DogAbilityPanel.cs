using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Deft;
using Deft.UI;
using Deft.Input;

// TODO(Matt): The targeting here should possibly be offloaded to DogController
public class DogAbilityPanel : UIPanel
{
    public DogController controller;

    AbilityButton sprintButton;
    AbilityButton lungeButton;

    bool isChoosingLungeTarget;

    protected override void OnAwake()
    {
        sprintButton = Find<AbilityButton>("SprintButton");
        lungeButton = Find<AbilityButton>("LungeButton");

        sprintButton.onClick.AddListener(OnSprintButtonClicked);
        lungeButton.onClick.AddListener(OnLungeButtonClicked);
    }

    void OnSprintButtonClicked()
    {
        if (controller.ActivateSprint())
            sprintButton.border.enabled = true;
    }

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
                }
            }
        }
    }

    protected override void OnVisibilityChanged(bool value)
    {
        if (value)
            SubscribeInputEvents();
        else
            UnsubscribeInputEvents();
    }

    void SubscribeInputEvents()
    {
        controller.target.eventSprintStart += OnDogSprintStart;
        controller.target.eventSprintEnd += OnDogSprintEnd;
        InputManager.Get.eventMousePressed += OnMousePressed;
    }

    void UnsubscribeInputEvents()
    {
        controller.target.eventSprintStart -= OnDogSprintStart;
        controller.target.eventSprintEnd -= OnDogSprintEnd;
        InputManager.Get.eventMousePressed -= OnMousePressed;
    }
}
