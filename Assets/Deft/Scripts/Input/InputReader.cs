using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Deft.Input
{
    public abstract class InputReader
    {
        public abstract bool AnyActionDetected();
        public abstract InputSnapshot GenerateInputSnapshot();
        public virtual Vector2 ScanNavigate() => Vector2.zero;

        public static InputState ScanButton(ButtonControl button)
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
}

