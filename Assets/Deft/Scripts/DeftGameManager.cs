using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deft
{
    public class DeftGameManager : Manager<DeftGameManager>
    {
        public delegate void GameStateChangedHandler(GameState oldState, GameState newState);
        public event GameStateChangedHandler eventGameStateChanged;

        public GameState State { get; private set; }

        public void SetState(GameState newState, bool forceState = false)
        {
            if (State != newState || forceState)
            {
                GameState oldState = State;
                State = newState;

                eventGameStateChanged?.Invoke(oldState, newState);
            }
        }
    }
}
