using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deft
{
    public class DeftSimulationManager : Manager<DeftSimulationManager>
    {
        public event System.Action eventEnterGame;
        public event System.Action eventExitGame;

        private bool isPaused;
        public bool IsPaused
        {
            get => isPaused;
            set
            {
                isPaused = value;
                // TODO: Actually pause.. Time scale?
            }
        }        

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

        protected virtual void OnActionPressed(int actionID) { }
        protected virtual void LoadGame() { }
        protected virtual void UnloadGame() { }
    }
}
