using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;

public class GameManager : DeftGameManager
{
    public override void OnAwake()
        => eventGameStateChanged += OnGameStateChanged;

    public override void OnStart()
        => SetState(GameState.MainMenu, true);

    private void OnGameStateChanged(GameState oldState, GameState newState)
    {
        if (newState == GameState.MainMenu)
            UIManager.Get.Show<MainMenuPanel>();
    }
}
