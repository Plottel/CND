using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;
using Deft.Input;
using Deft.UI;
using TMPro;

public class HotkeysPanel : UIPanel
{
    private RectTransform hotkeyTemplate;

    private RectTransform hotkeyContainer;

    public UIHotkeyButton primary;
    public UIHotkeyButton moveLeft;
    public UIHotkeyButton moveRight;
    public UIHotkeyButton moveUp;
    public UIHotkeyButton moveDown;

    private List<UIHotkeyButton> hotkeyButtons;
    private UIHotkeyButton selectedButton;

    protected override void OnAwake()
    {
        hotkeyContainer = Find<RectTransform>("HotkeysContainer");
        hotkeyTemplate = hotkeyContainer.Find<RectTransform>("HotkeyTemplate");

        primary = Find<UIHotkeyButton>("PrimaryHotkey");
        moveLeft = Find<UIHotkeyButton>("MoveLeftHotkey");
        moveRight = Find<UIHotkeyButton>("MoveRightHotkey");
        moveUp = Find<UIHotkeyButton>("MoveUpHotkey");
        moveDown = Find<UIHotkeyButton>("MoveDownHotkey");

        hotkeyButtons = new List<UIHotkeyButton>
        {
            primary,
            moveLeft,
            moveRight,
            moveUp,
            moveDown
        };

        foreach (var button in hotkeyButtons)
            button.onClick.AddListener(() => OnHotkeyButtonClicked(button));
    }

    protected override void OnStart()
    {
        InputManager.Get.eventInputDeviceChanged += (deviceType) =>
        {
            RefreshPanel();
        };
    }

    protected override void OnVisibilityChanged(bool value)
    {
        if (value)
            RefreshPanel();
    }

    void OnHotkeyButtonClicked(UIHotkeyButton button)
    {
        selectedButton = button;
        UIManager.Get.PushModal<AnyKeyModalPanel, string>(OnAnyKeyModalPopped);
    }

    void OnAnyKeyModalPopped(string controlID)
    {
        selectedButton.ControlName = controlID;
        selectedButton = null;
    }

    void RefreshPanel()
    {
        var reader = InputManager.Get.GetActiveReader<PlayerInputReader>();

        primary.ControlName = reader.GetControlID(PlayerActions.Primary);
        moveLeft.ControlName = reader.GetControlID(PlayerActions.Movement, Direction.Left);
        moveRight.ControlName = reader.GetControlID(PlayerActions.Movement, Direction.Right);
        moveUp.ControlName = reader.GetControlID(PlayerActions.Movement, Direction.Up);
        moveDown.ControlName = reader.GetControlID(PlayerActions.Movement, Direction.Down);
    }
}
