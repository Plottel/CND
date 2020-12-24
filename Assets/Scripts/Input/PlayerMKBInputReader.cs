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

    public ButtonControl primary;
    public KeyboardDpadControl movement;

    private List<ButtonControl> actionControls;

    public PlayerMKBInputReader(Mouse mouse, Keyboard kb)
    {
        this.mouse = mouse;
        this.kb = kb;

        movement = new KeyboardDpadControl();
        actionControls = new List<ButtonControl>();

        // Force setting m_Device so Unity doesn't whinge that it's not an "official" control
        var deviceField = movement.GetType().GetField("m_Device", BindingFlags.NonPublic | BindingFlags.Instance);
        deviceField.SetValue(movement, kb);        
    }    

    // TODO: Proper mapping here. Does it need to be uniform with Gamepad reader?
    public void BindAction(int actionID, string controlID, Direction direction = Direction.None)
    {
        if (actionID == PlayerActions.Primary)
            primary = Find<ButtonControl>(controlID);
        else if (actionID == PlayerActions.Movement)
            movement.BindControl(Find<ButtonControl>(controlID), direction);

        RefreshActionControls();
    }

    private void RefreshActionControls()
    {
        actionControls = new List<ButtonControl>
        {
            primary, 
            movement.left,
            movement.right,
            movement.up,
            movement.down
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
