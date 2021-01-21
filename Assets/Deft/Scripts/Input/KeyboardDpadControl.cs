using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Deft.Input
{
    public class KeyboardDpadControl : InputControl<Vector2>
    {
        static KeyboardDpadControl() => InputSystem.RegisterLayout<KeyboardDpadControl>();

        public ButtonControl left;
        public ButtonControl right;
        public ButtonControl up;
        public ButtonControl down;

        public override unsafe Vector2 ReadUnprocessedValueFromState(void* statePtr)
            => DpadControl.MakeDpadVector
                (
                    up.ReadValue(), 
                    down.ReadValue(), 
                    left.ReadValue(), 
                    right.ReadValue()
                );

        public string GetControlID(Direction direction)
        {
            if (direction == Direction.Up)          return up.name;
            else if (direction == Direction.Down)   return down.name;
            else if (direction == Direction.Left)   return left.name;
            else if (direction == Direction.Right)  return right.name;
            return "";
        }

        public void BindControl(ButtonControl control, Direction direction)
        {
            if (direction == Direction.Up)          up = control;
            else if (direction == Direction.Down)   down = control;
            else if (direction == Direction.Left)   left = control;
            else if (direction == Direction.Right)  right = control;
        }
    }
}
