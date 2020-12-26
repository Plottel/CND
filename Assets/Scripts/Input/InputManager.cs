using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Deft;
using Deft.Input;

public class InputManager : DeftInputManager
{
    private PlayerMKBInputReader mkbReader;
    private PlayerGamepadInputReader gamepadReader;

    private PlayerInputProfile mkbProfile;
    private PlayerInputProfile gamepadProfile;

    private Keyboard kb;
    private Mouse mouse;
    private Gamepad gamepad;

    public override void OnAwake()
    {
        base.OnAwake();

        kb = Keyboard.current;
        mouse = Mouse.current;
        gamepad = Gamepad.current;

        LoadInputProfiles();
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

    void LoadInputProfiles()
    {
        mkbProfile = new PlayerInputProfile();
        mkbProfile.moveLeft = "a";
        mkbProfile.moveRight = "d";
        mkbProfile.moveUp = "w";
        mkbProfile.moveDown = "s";
        mkbProfile.primary = "leftButton";

        gamepadProfile = new PlayerInputProfile();
        gamepadProfile.moveLeft = "leftStick/left";
        gamepadProfile.moveRight = "leftStick/right";
        gamepadProfile.moveUp = "leftStick/up";
        gamepadProfile.moveDown = "leftStick/down";
        gamepadProfile.primary = "buttonSouth";
    }

    void SetupInputReaders()
    {
        mkbReader = new PlayerMKBInputReader(mouse, kb);
        gamepadReader = new PlayerGamepadInputReader(gamepad);

        SetupMKBInputReader(mkbReader, mkbProfile);
        SetupGamepadInputReader(gamepadReader, gamepadProfile);
    }

    void SetupMKBInputReader(PlayerMKBInputReader reader, PlayerInputProfile profile)
    {
        foreach (string id in InputMappings.KeyboardControlIDs)
            reader.AddInputControl(kb.GetChildControl(id));

        foreach (string id in InputMappings.MouseControlIDs)
            reader.AddInputControl(mouse.GetChildControl(id));        

        reader.BindAction(PlayerActions.Movement, profile.moveLeft, Direction.Left);
        reader.BindAction(PlayerActions.Movement, profile.moveRight, Direction.Right);
        reader.BindAction(PlayerActions.Movement, profile.moveUp, Direction.Up);
        reader.BindAction(PlayerActions.Movement, profile.moveDown, Direction.Down);
        reader.BindAction(PlayerActions.Primary, profile.primary);
    }

    void SetupGamepadInputReader(PlayerGamepadInputReader reader, PlayerInputProfile profile)
    {
        foreach (string id in InputMappings.GamepadControlIDs)
            reader.AddInputControl(gamepad.GetChildControl(id));        

        reader.BindAction(PlayerActions.Movement, profile.moveLeft, Direction.Left);
        reader.BindAction(PlayerActions.Movement, profile.moveRight, Direction.Right);
        reader.BindAction(PlayerActions.Movement, profile.moveUp, Direction.Up);
        reader.BindAction(PlayerActions.Movement, profile.moveDown, Direction.Down);
        reader.BindAction(PlayerActions.Primary, profile.primary);
    }
}
