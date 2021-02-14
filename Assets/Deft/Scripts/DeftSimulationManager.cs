using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deft
{
    public class DeftSimulationManager : Manager<DeftSimulationManager>
    {
        public event System.Action eventEnterGame;
        public event System.Action eventExitGame;
        public event System.Action<bool> eventPause;

        public bool IsPaused { get; protected set; }        

        public void EnterGame()
        {
            GameManager.Get.SetState(GameState.Loading);

            LoadGame();

            GameManager.Get.SetState(GameState.InGame);

            InputManager.Get.eventActionPressed += OnActionPressed;
            eventEnterGame?.Invoke();
        }

        public void ExitGame()
        {
            eventExitGame?.Invoke();
            InputManager.Get.eventActionPressed -= OnActionPressed;

            UnloadGame();

            GameManager.Get.SetState(GameState.MainMenu);
        }

        public void Pause(bool pause)
        {
            if (IsPaused != pause)
            {
                IsPaused = pause;
                eventPause?.Invoke(pause);
            }
        }

        public void TogglePause() => Pause(!IsPaused);

        protected virtual void OnActionPressed(int actionID) { }
        protected virtual void LoadGame() { }
        protected virtual void UnloadGame() { }
    }
}
