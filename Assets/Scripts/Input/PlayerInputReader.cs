﻿using System.Reflection;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Deft.Input;

public abstract class PlayerInputReader : InputReader
{
    protected List<InputControl> inputControls = new List<InputControl>();
    protected List<ButtonControl> actionControls = new List<ButtonControl>();

    protected InputDevice device;

    public ButtonControl primary;
    public ButtonControl secondary;
    public KeyboardDpadControl movement;
    public ButtonControl start;

    public PlayerInputReader(InputDevice device)
    {
        this.device = device;
        movement = new KeyboardDpadControl();

        // Force setting m_Device so Unity doesn't whinge that it's not an "official" control
        var deviceField = movement.GetType().GetField("m_Device", BindingFlags.NonPublic | BindingFlags.Instance);
        deviceField.SetValue(movement, device);
    }

    public void AddInputControl(InputControl control) => inputControls.Add(control);

    public T Find<T>(string id) where T : InputControl
    {
        foreach (InputControl control in inputControls)
        {
            if (control.name == id)
                return control as T;
            else if (id.Contains("/")) // Is this a sub-control e.g. leftStick/left?
            {
                string fullName = control.parent.name + "/" + control.name;
                if (fullName == id)
                    return control as T;
            }
        }

        return null;
    }

    public string GetControlID(int actionID, Direction direction = Direction.None)
    {
        if (actionID == PlayerActions.Primary)
            return primary.name;
        else if (actionID == PlayerActions.Secondary)
            return secondary.name;
        else if (actionID == PlayerActions.Movement)
            return movement.GetControlID(direction);
        return "";
    }

    //public void BindAxis(int actionID, string axisRootID)
    //{
    //    if (actionID == PlayerActions.Primary)
    //        primary = Find<ButtonControl>(axisRootID);
    //    else if (actionID == PlayerActions.Secondary)
    //        secondary = Find<ButtonControl>(axisRootID);
    //    else if (actionID == PlayerActions.Movement)
    //    {
    //        movement.BindControl(Find<ButtonControl>(axisRootID + "/left"), Direction.Left);
    //        movement.BindControl(Find<ButtonControl>(axisRootID + "/right"), Direction.Right);
    //        movement.BindControl(Find<ButtonControl>(axisRootID + "/up"), Direction.Up);
    //        movement.BindControl(Find<ButtonControl>(axisRootID + "/down"), Direction.Down);
    //    }

    //    RefreshActionControls();
    //}

    public void BindAction(int actionID, string controlID, Direction direction = Direction.None)
    {
        if (actionID == PlayerActions.Primary)
            primary = Find<ButtonControl>(controlID);
        else if (actionID == PlayerActions.Secondary)
            secondary = Find<ButtonControl>(controlID);
        else if (actionID == PlayerActions.Movement)
            movement.BindControl(Find<ButtonControl>(controlID), direction);
        else if (actionID == PlayerActions.Start)
            start = Find<ButtonControl>(controlID);

        RefreshActionControls();
    }

    public void RefreshActionControls()
    {
        actionControls = new List<ButtonControl>
        {
            primary,
            secondary,
            movement.left,
            movement.right,
            movement.up,
            movement.down,
            start
        };
    }

    public override bool AnyActionDetected()
    {
        if (device == null)
            return false;

        foreach (var button in actionControls)
        {
            if (ScanButton(button) != InputState.None)
                return true;
        }
        return false;
    }

    public bool AnyControlPressed(out InputControl pressedControl)
    {
        foreach (var control in inputControls)
        {
            if (control.IsPressed())
            {
                pressedControl = control;
                return true;
            }
        }

        pressedControl = null;
        return false;
    }
}
