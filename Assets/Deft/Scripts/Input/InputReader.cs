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
    }
}

