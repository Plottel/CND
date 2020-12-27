using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Deft;
using Deft.Input;

public class InputManager : DeftInputManager
{
    private string kCustomProfilePath;

    private Keyboard kb;
    private Mouse mouse;
    private Gamepad gamepad;

    private PlayerMKBInputReader mkbReader;
    private PlayerGamepadInputReader gamepadReader;

    [SerializeField] private PlayerInputProfile defaultMkbProfile = default;
    [SerializeField] private PlayerInputProfile defaultGamepadProfile = default;

    private PlayerInputProfile mkbProfile;
    private PlayerInputProfile gamepadProfile;

    public override void OnAwake()
    {
        base.OnAwake();

        kCustomProfilePath = Application.persistentDataPath + "/customprofile.input";

        kb = Keyboard.current;
        mouse = Mouse.current;
        gamepad = Gamepad.current;

        SetupInputReaders();
        AddReader(InputDeviceType.MouseKeyboard, mkbReader);
        AddReader(InputDeviceType.Gamepad, gamepadReader);

        SetupInputProfiles();
        ApplyInputProfile(mkbReader, mkbProfile);
        ApplyInputProfile(gamepadReader, gamepadProfile);

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

        foreach (string id in InputMappings.GamepadControlIDs)
            gamepadReader.AddInputControl(gamepad.GetChildControl(id));
    }

    void SetupInputProfiles()
    {
        if (File.Exists(kCustomProfilePath)) // Try load custom profile 
        {
            StreamReader reader = new StreamReader(kCustomProfilePath);
            mkbProfile = new PlayerInputProfile();
            gamepadProfile = new PlayerInputProfile();

            mkbProfile.Deserialize(reader);
            gamepadProfile.Deserialize(reader);

            reader.Close();
            reader.Dispose();
        }
        else // Load default profiles
        {
            mkbProfile = defaultMkbProfile;
            gamepadProfile = defaultGamepadProfile;
        }
    }

    void ApplyInputProfile(PlayerInputReader reader, PlayerInputProfile profile)
    {
        reader.BindAction(PlayerActions.Movement, profile.moveLeft, Direction.Left);
        reader.BindAction(PlayerActions.Movement, profile.moveRight, Direction.Right);
        reader.BindAction(PlayerActions.Movement, profile.moveUp, Direction.Up);
        reader.BindAction(PlayerActions.Movement, profile.moveDown, Direction.Down);
        reader.BindAction(PlayerActions.Primary, profile.primary);
    }

    void SaveInputProfiles()
    {
        FileStream file = File.Open(kCustomProfilePath, FileMode.OpenOrCreate);
        
        using (var writer = new StreamWriter(file))
        {
            mkbProfile.Serialize(writer);
            gamepadProfile.Serialize(writer);
        }

        file.Close();
    }
}
