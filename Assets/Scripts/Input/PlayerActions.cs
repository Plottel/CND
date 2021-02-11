using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO(Matt): Add Secondary
public static class PlayerActions
{
    public const int Movement = 0;
    public const int Primary = 1;
    public const int Start = 2;
    public const int Count = 3;

    public static string[] Names { get; } = new string[]
    {
        "Movement", "Primary"
    };

    public static string GetName(int actionID) => Names[actionID];
}
