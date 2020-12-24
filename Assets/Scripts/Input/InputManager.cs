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

    void SetupInputReaders()
    {
        mkbReader = new PlayerMKBInputReader(mouse, kb);
        gamepadReader = new PlayerGamepadInputReader(gamepad);

        foreach (string id in InputMappings.KeyboardControlIDs)
            mkbReader.AddInputControl(kb.GetChildControl(id));

        foreach (string id in InputMappings.MouseControlIDs)
            mkbReader.AddInputControl(mouse.GetChildControl(id));

        mkbReader.BindAction(PlayerActions.Movement, "a", Direction.Left);
        mkbReader.BindAction(PlayerActions.Movement, "d", Direction.Right);
        mkbReader.BindAction(PlayerActions.Movement, "w", Direction.Up);
        mkbReader.BindAction(PlayerActions.Movement, "s", Direction.Down);
        mkbReader.BindAction(PlayerActions.Primary, "leftButton");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //if (kb.gKey.wasPressedThisFrame) SetActiveScheme(InputScheme.Gameplay);
        //if (kb.uKey.wasPressedThisFrame) SetActiveScheme(InputScheme.UI);
    }
}
