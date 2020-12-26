using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Deft.Input;

public class PlayerGamepadInputReader : PlayerInputReader
{
    private Gamepad gamepad;

    public PlayerGamepadInputReader(Gamepad gamepad) : base(gamepad)
        => this.gamepad = gamepad;

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
