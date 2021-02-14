using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Deft.Input;

public class PlayerMKBInputReader : PlayerInputReader
{
    private Mouse mouse;
    private Keyboard kb;

    public PlayerMKBInputReader(Mouse mouse, Keyboard kb) : base(kb)
    {
        this.mouse = mouse;
        this.kb = kb;        
    }    

    public override InputSnapshot GenerateInputSnapshot()
    {
        var result = new InputSnapshot(PlayerActions.Count);

        result.SetActionSnapshot(PlayerActions.Movement, GetMovementSnapshot());
        result.SetActionSnapshot(PlayerActions.Primary, GetPrimarySnapshot());
        result.SetActionSnapshot(PlayerActions.Secondary, GetSecondarySnapshot());
        result.SetActionSnapshot(PlayerActions.Start, GetStartSnapshot());

        return result;
    }

    public override Vector2 ScanNavigate()
    {
        return movement.ReadValue();
    }

    ActionSnapshot GetMovementSnapshot() =>
        new ActionSnapshot
        {
            actionID = PlayerActions.Movement,
            type = InputType.Axis,
            state = InputState.Down, // Guarantee live value is always sent.
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

    ActionSnapshot GetSecondarySnapshot() =>
        new ActionSnapshot
        {
            actionID = PlayerActions.Secondary,
            type = InputType.Button,
            state = ScanButton(secondary),
            axis = Vector2.zero
        };

    ActionSnapshot GetStartSnapshot() =>
    new ActionSnapshot
    {
        actionID = PlayerActions.Start,
        type = InputType.Button,
        state = ScanButton(start),
        axis = Vector2.zero
    };
}
