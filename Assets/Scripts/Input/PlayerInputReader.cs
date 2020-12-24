using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Deft.Input;

public abstract class PlayerInputReader : InputReader
{
    protected List<InputControl> inputControls = new List<InputControl>();    

    public void AddInputControl(InputControl control) => inputControls.Add(control);

    public T Find<T>(string id) where T : InputControl
    {
        foreach (InputControl control in inputControls)
        {
            if (control.name == id)
                return control as T;
        }

        return null;
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

    public InputState ScanButton(ButtonControl button)
    {
        if (button.wasPressedThisFrame)
            return InputState.Pressed;
        else if (button.wasReleasedThisFrame)
            return InputState.Released;
        else if (button.isPressed)
            return InputState.Down;

        return InputState.None;
    }
}
