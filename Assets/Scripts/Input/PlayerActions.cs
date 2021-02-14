using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO(Matt): Add Secondary
public static class PlayerActions
{
    public const int Movement = 0;
    public const int Primary = 1;
    public const int Secondary = 2;
    public const int Start = 3;
    public const int Count = 4;

    public static string[] Names { get; } = new string[]
    {
        "Movement", "Primary", "Secondary", "Start"
    };

    public static string GetName(int actionID) => Names[actionID];
}
