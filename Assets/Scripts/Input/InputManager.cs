using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Deft;
using Deft.Input;

public class InputManager : DeftInputManager
{
    private PlayerMKBInputReader mkbReader;
    private PlayerGamepadInputReader gamepadReader;

    private Keyboard kb;
    private Mouse mouse;
    private Gamepad gamepad;

    public override void OnAwake()
    {
        base.OnAwake();

        kb = Keyboard.current;
        mouse = Mouse.current;
        gamepad = Gamepad.current;

        SetupInputReaders();

        AddReader(InputDeviceType.MouseKeyboard, mkbReader);
        AddReader(InputDeviceType.Gamepad, gamepadReader);

        SetActiveDevice(InputDeviceType.MouseKeyboard, true);
        SetActiveScheme(InputScheme.Gameplay);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //if (kb.gKey.wasPressedThisFrame) SetActiveScheme(InputScheme.Gameplay);
        //if (kb.uKey.wasPressedThisFrame) SetActiveScheme(InputScheme.UI);
    }

    void SetupInputReaders()
    {
        mkbReader = new PlayerMKBInputReader(mouse, kb);
        gamepadReader = new PlayerGamepadInputReader(gamepad);

        SetupMKBInputReader(mkbReader);
        SetupGamepadInputReader(gamepadReader);
    }

    void SetupMKBInputReader(PlayerMKBInputReader reader)
    {
        foreach (string id in InputMappings.KeyboardControlIDs)
            reader.AddInputControl(kb.GetChildControl(id));

        foreach (string id in InputMappings.MouseControlIDs)
            reader.AddInputControl(mouse.GetChildControl(id));

        reader.BindAction(PlayerActions.Movement, "a", Direction.Left);
        reader.BindAction(PlayerActions.Movement, "d", Direction.Right);
        reader.BindAction(PlayerActions.Movement, "w", Direction.Up);
        reader.BindAction(PlayerActions.Movement, "s", Direction.Down);
        reader.BindAction(PlayerActions.Primary, "leftButton");
    }

    void SetupGamepadInputReader(PlayerGamepadInputReader reader)
    {
        foreach (string id in InputMappings.GamepadControlIDs)
            reader.AddInputControl(gamepad.GetChildControl(id));

        reader.BindAction(PlayerActions.Movement, "leftStick/left", Direction.Left);
        reader.BindAction(PlayerActions.Movement, "leftStick/right", Direction.Right);
        reader.BindAction(PlayerActions.Movement, "leftStick/up", Direction.Up);
        reader.BindAction(PlayerActions.Movement, "leftStick/down", Direction.Down);
        reader.BindAction(PlayerActions.Primary, "buttonSouth");
    }
}
