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

        return result;
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
}
