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

    public UIHotkeyEntry primary;
    public UIHotkeyEntry moveLeft;
    public UIHotkeyEntry moveRight;
    public UIHotkeyEntry moveUp;
    public UIHotkeyEntry moveDown;

    protected override void OnAwake()
    {
        hotkeyContainer = Find<RectTransform>("HotkeysContainer");
        hotkeyTemplate = hotkeyContainer.Find<RectTransform>("HotkeyTemplate");

        primary = Find<UIHotkeyEntry>("PrimaryHotkey");
        moveLeft = Find<UIHotkeyEntry>("MoveLeftHotkey");
        moveRight = Find<UIHotkeyEntry>("MoveRightHotkey");
        moveUp = Find<UIHotkeyEntry>("MoveUpHotkey");
        moveDown = Find<UIHotkeyEntry>("MoveDownHotkey");
    }

    protected override void OnStart()
    {
        InputManager.Get.eventInputDeviceChanged += (deviceType) =>
        {
            RefreshPanel();
        };
    }

    private void Get_eventInputDeviceChanged(InputDeviceType deviceType)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnVisibilityChanged(bool value)
    {
        if (value)
            RefreshPanel();
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
