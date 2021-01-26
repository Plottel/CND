using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;
using Deft.Input;

public class GameManager : DeftGameManager<GameManager>
{
    public event System.Action eventEnterGame;

    public override void OnAwake()
    {
        eventGameStateChanged += OnGameStateChanged;
    }

    public override void OnStart()
        => SetState(GameState.MainMenu, true);

    private void OnGameStateChanged(GameState oldState, GameState newState)
    {
        UIManager.Get.CloseAllPanels();

        switch (newState)
        {
            case GameState.MainMenu:
                InputManager.Get.SetActiveScheme(InputScheme.Menu);
                UIManager.Get.Show<MainMenuPanel>();
                break;

            case GameState.InGame:
                InputManager.Get.SetActiveScheme(InputScheme.Gameplay);
                eventEnterGame?.Invoke();
                break;

            case GameState.Loading:
                UIManager.Get.Show<LoadingScreenPanel>();
                break;
        }
    }
}
