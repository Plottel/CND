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

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        void Initialize() { }

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

        public void BindControl(ButtonControl control, Direction direction)
        {
            if (direction == Direction.Up) up = control;
            else if (direction == Direction.Down) down = control;
            else if (direction == Direction.Left) left = control;
            else if (direction == Direction.Right) right = control;
        }
    }
}
