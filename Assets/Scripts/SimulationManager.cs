using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;
using Deft.Input;

public class SimulationManager : Manager<SimulationManager>
{
    public Dog DogPrefab;

    Dog dog;
    DogController controller;

    PausePanel pausePanel;

    public event System.Action eventEnterGame;
    public event System.Action eventExitGame;

    private bool isPaused;
    public bool IsPaused
    {
        get => isPaused;
        set
        {
            isPaused = value;
            //Time.timeScale = isPaused ? 0 : 1;
            //InputManager.Get.SetActiveScheme(isPaused ? InputScheme.Menu : InputScheme.Gameplay);
        }
    }

    public override void OnStart()
    {
        GameManager.Get.eventGameStateChanged += OnGameStateChanged;
        pausePanel = UIManager.Get.GetPanel<PausePanel>();
    }

    private void OnGameStateChanged(GameState oldState, GameState newState)
    {
        if (newState == GameState.InGame)
        {
            OnEnterGame();
            eventEnterGame?.Invoke();
        }
    }

    private void OnEnterGame()
    {
        dog = Instantiate(DogPrefab);
        controller = new GameObject().AddComponent<DogController>();
        controller.target = dog;

        InputManager.Get.eventActionPressed += OnActionPressed;
    }

    public void ExitToMainMenu()
    {
        InputManager.Get.eventActionDown -= OnActionPressed;

        eventExitGame?.Invoke();
        GameManager.Get.SetState(GameState.Loading);
        GameManager.Get.SetState(GameState.MainMenu);
    }

    void OnActionPressed(int actionID)
    {
        if (actionID == PlayerActions.Start)
        {
            IsPaused = !IsPaused;
            pausePanel.IsVisible = IsPaused;
        }
    }
}
