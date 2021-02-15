using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.EventSystems;
using Deft.Input;

namespace Deft
{
    public class DeftInputManager : Manager<DeftInputManager>
    {
        public delegate void MouseButtonHandler(int buttonID, Vector2 pos);
        public event MouseButtonHandler eventMousePressed;
        public event MouseButtonHandler eventMouseDown;
        public event MouseButtonHandler eventMouseReleased;

        protected Keyboard kb;
        protected Mouse mouse;
        protected Gamepad gamepad;
        protected Camera mainCamera;
        protected EventSystem eventSystem;

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
        public InputDeviceType activeDeviceType { get; private set; }
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
            kb = Keyboard.current;
            mouse = Mouse.current;
            gamepad = Gamepad.current;
            eventSystem = EventSystem.current;

            inputReaders = new InputReader[2];
        }

        public override void OnStart()
        {
            mainCamera = CameraManager.Get.MainCamera;
        }

        public override void OnUpdate()
        {
            ChangeActiveInputReaderIfActionDetected();

            Vector2 mousePos = mainCamera.ScreenToWorldPoint(mouse.position.ReadValue());

            TriggerMouseEvents(0, mouse.leftButton, mousePos);
            TriggerMouseEvents(1, mouse.rightButton, mousePos);

            if (activeScheme == InputScheme.Gameplay)
            {
                inputSnapshot = activeReader.GenerateInputSnapshot();
                ProcessInputSnapshot(inputSnapshot);
            }
            else if (activeScheme == InputScheme.Menu)
            {
                // TODO(Matt): Does anything need to happen here?
            }
        }

        void TriggerMouseEvents(int buttonID, ButtonControl button, Vector2 mousePos)
        {
            switch (InputReader.ScanButton(button))
            {
                case InputState.Pressed: eventMousePressed?.Invoke(buttonID, mousePos); break;
                case InputState.Down: eventMouseDown?.Invoke(buttonID, mousePos); break;
                case InputState.Released: eventMouseReleased?.Invoke(buttonID, mousePos); break;
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

        private void ChangeActiveInputReaderIfActionDetected()
        {
            for (int i = 0; i < inputReaders.Length; ++i)
            {
                if (i == (int)activeDeviceType)
                    continue;

                if (inputReaders[i].AnyActionDetected())
                    SetActiveDevice((InputDeviceType)i);
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

