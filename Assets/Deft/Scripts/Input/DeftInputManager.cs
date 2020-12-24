using System;
using System.Collections.Generic;
using UnityEngine;
using Deft.Input;

namespace Deft
{
    public class DeftInputManager : Manager<DeftInputManager>
    {
        public delegate void InputDeviceChangedHandler(InputDeviceType deviceType);
        public event InputDeviceChangedHandler eventInputDeviceChanged;

        public delegate void InputSchemeChangedHandler(InputScheme scheme);
        public event InputSchemeChangedHandler eventInputSchemeChanged;

        public delegate void ActionAxisHandler(int actionID, Vector2 axis);
        public delegate void ActionButtonHandler(int actionID);

        public event ActionAxisHandler eventAxisMoved;
        public event ActionButtonHandler eventActionPressed;
        public event ActionButtonHandler eventActionDown;
        public event ActionButtonHandler eventActionReleased;

        private InputReader[] inputReaders;
        public InputReader activeReader { get; private set; }
        private InputDeviceType activeDeviceType;
        public InputScheme activeScheme { get; private set; }

        public InputSnapshot inputSnapshot { get; private set; }

        public T GetReader<T>() where T : InputReader
        {
            foreach (InputReader reader in inputReaders)
            {
                T result = reader as T;
                if (result != null)
                    return result;
            }
            return null;
        }

        public T GetActiveReader<T>() where T : InputReader
        {
            return activeReader as T;
        }

        public override void OnAwake()
        {
            inputReaders = new InputReader[2];
            base.OnAwake();
        }

        public override void OnUpdate()
        {
            UpdateActiveInputDevice();

            if (activeScheme == InputScheme.Gameplay)
            {
                inputSnapshot = activeReader.GenerateInputSnapshot();
                ProcessInputSnapshot(inputSnapshot);
            }
            else if (activeScheme == InputScheme.UI)
            {
                inputSnapshot = new InputSnapshot(PlayerActions.Count);
                // Record input and forward it to UI input module somehow?
                // Am I overcomplicating this?
            }
        }

        private void ProcessInputSnapshot(InputSnapshot inputSnapshot)
        {
            foreach (ActionSnapshot action in inputSnapshot.actionSnapshots)
            {
                if (action.state == InputState.None)
                    continue;

                switch (action.type)
                {
                    case InputType.Button:
                        switch (action.state)
                        {
                            case InputState.Pressed: eventActionPressed?.Invoke(action.actionID); eventActionDown?.Invoke(action.actionID); break;
                            case InputState.Down: eventActionDown?.Invoke(action.actionID); break;
                            case InputState.Released: eventActionReleased?.Invoke(action.actionID); break;
                        }
                        break;

                    case InputType.Axis: eventAxisMoved?.Invoke(action.actionID, action.axis); break;
                }
            }
        }

        private void UpdateActiveInputDevice()
        {
            if (activeDeviceType == InputDeviceType.MouseKeyboard)
            {
                if (inputReaders[(int)InputDeviceType.Gamepad].AnyActionDetected())
                    SetActiveDevice(InputDeviceType.Gamepad);
            }
            else if (activeDeviceType == InputDeviceType.Gamepad)
            {
                if (inputReaders[(int)InputDeviceType.MouseKeyboard].AnyActionDetected())
                    SetActiveDevice(InputDeviceType.MouseKeyboard);
            }
        }

        public void AddReader(InputDeviceType deviceType, InputReader reader) =>
            inputReaders[(int)deviceType] = reader;

        public void SetActiveDevice(InputDeviceType deviceType, bool force = false)
        {
            if (activeDeviceType != deviceType || force)
            {
                activeDeviceType = deviceType;
                activeReader = inputReaders[(int)deviceType];
                eventInputDeviceChanged?.Invoke(deviceType);
            }
        }

        public void SetActiveScheme(InputScheme scheme)
        {
            activeScheme = scheme;
            eventInputSchemeChanged?.Invoke(scheme);
        }
    }
}

