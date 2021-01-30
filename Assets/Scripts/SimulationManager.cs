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
            // TODO: Actually pause.. Time scale?
        }
    }

    public override void OnStart()
        => pausePanel = UIManager.Get.GetPanel<PausePanel>();

    public void EnterGame()
    {
        GameManager.Get.SetState(GameState.Loading);

        dog = Instantiate(DogPrefab);
        controller = new GameObject().AddComponent<DogController>();
        controller.target = dog;

        GameManager.Get.SetState(GameState.InGame);

        InputManager.Get.eventActionPressed += OnActionPressed;
        eventEnterGame?.Invoke();
    }

    public void ExitGame()
    {
        eventExitGame?.Invoke();
        InputManager.Get.eventActionPressed -= OnActionPressed;

        Destroy(dog.gameObject);
        Destroy(controller.gameObject);

        dog = null;
        controller = null;

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
