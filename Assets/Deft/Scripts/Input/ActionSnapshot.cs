using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deft.Input
{
    public struct ActionSnapshot
    {
        public int actionID;
        public InputType type;
        public InputState state;
        public Vector2 axis;
    }
}