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

    public DogController controller { get; private set; }

    AbilityButton sprintButton;
    AbilityButton lungeButton;

    protected override void OnAwake()
    {
        inputAtlas = UIManager.Get.inputAtlas;

        sprintButton = Find<AbilityButton>("SprintButton");
        lungeButton = Find<AbilityButton>("LungeButton");

        sprintButton.onClick.AddListener(OnSprintButtonClicked);
        lungeButton.onClick.AddListener(OnLungeButtonClicked);
    }

    public void SetController(DogController newController)
    {
        controller = newController;

        if (controller == null)
            return;

        sprintButton.SetAbility(controller.target.GetAbility(PlayerActions.Primary));
        lungeButton.SetAbility(controller.target.GetAbility(PlayerActions.Secondary));
    }

    void OnSprintButtonClicked() => controller.UseAbility(PlayerActions.Primary);
    void OnLungeButtonClicked() => controller.BeginChoosingTarget(PlayerActions.Secondary);

    void RefreshInputSprites(InputDeviceType deviceType)
    {
        var reader = InputManager.Get.GetActiveReader<PlayerInputReader>();

        SetInputSprite(PlayerActions.Primary, sprintButton.hotkey);
        SetInputSprite(PlayerActions.Secondary, lungeButton.hotkey);

        void SetInputSprite(int actionID, Image image)
        {
            string controlID = reader.GetControlID(actionID);
            string thumbName = string.Concat("thumb-", controlID).ToLower();

            image.sprite = inputAtlas.GetSprite(thumbName);
        }
    }

    protected override void OnVisibilityChanged(bool visible)
    {
        if (visible)
        {
            RefreshInputSprites(InputManager.Get.activeDeviceType);
            InputManager.Get.eventInputDeviceChanged += RefreshInputSprites;
        }
        else
            InputManager.Get.eventInputDeviceChanged -= RefreshInputSprites;
    }
}
