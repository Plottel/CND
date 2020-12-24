using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Deft.Input;

public class PlayerGamepadInputReader : PlayerInputReader
{
    private Gamepad gamepad;

    private StickControl movement;
    private ButtonControl primary;

    private List<ButtonControl> actionControls; 

    public PlayerGamepadInputReader(Gamepad gamepad)
    {
        this.gamepad = gamepad;

        movement = gamepad.leftStick;
        primary = gamepad.buttonSouth;

        actionControls = new List<ButtonControl>
        {
            primary
        };
    }

    public override bool AnyActionDetected()
    {
        foreach (var button in actionControls)
        {
            if (ScanButton(button) != InputState.None)
                return true;
        }
        return false;
    }

    public override InputSnapshot GenerateInputSnapshot()
    {
        var result = new InputSnapshot(PlayerActions.Count);

        result.SetActionSnapshot(PlayerActions.Movement, GetMovementSnapshot());
        result.SetActionSnapshot(PlayerActions.Primary, GetPrimarySnapshot());

        return result;
    }

    ActionSnapshot GetMovementSnapshot() =>
        new ActionSnapshot
        {
            actionID = PlayerActions.Movement,
            type = InputType.Axis,
            state = InputState.Down,
            axis = movement.ReadValue()
        };

    ActionSnapshot GetPrimarySnapshot() =>
        new ActionSnapshot
        {
            actionID = PlayerActions.Primary,
            type = InputType.Button,
            state = ScanButton(primary),
            axis = Vector2.zero
        };
}
