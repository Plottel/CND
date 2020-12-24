using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputMappings
{
    private static readonly string[] kKeyboardControlIDs = new string[]
    {
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "k",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
        "r",
        "s",
        "t",
        "u",
        "v",
        "w",
        "x",
        "y",
        "z"
    };
    private static readonly string[] kMouseControlIDs = new string[]
    {
        "leftButton",
        "middleButton",
        "rightButton"
    };

    public static string[] KeyboardControlIDs { get => kKeyboardControlIDs; }
    public static string[] MouseControlIDs { get => kMouseControlIDs; }
}
