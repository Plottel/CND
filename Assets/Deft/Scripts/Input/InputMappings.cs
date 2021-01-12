namespace Deft.Input
{
    public static class InputMappings
    {
        public static string[] KeyboardControlIDs { get; } = new string[]
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

        public static string[] MouseControlIDs { get; } = new string[]
        {
            "leftButton",
            "middleButton",
            "rightButton"
        };

        public static string[] GamepadControlIDs { get; } = new string[]
        {
            "buttonSouth",
            "buttonNorth",
            "buttonWest",
            "buttonEast",
            //"leftStick", // Do we care about sticks as a whole if we're 4-way splitting?
            "leftStick/up",
            "leftStick/down",
            "leftStick/left",
            "leftStick/right",
            "rightStick/up",
            "rightStick/down",
            "rightStick/left",
            "rightStick/right"
        };
    }
}
