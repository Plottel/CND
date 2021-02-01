using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;
using Deft.Input;

public class SimulationManager : DeftSimulationManager
{
    public Dog DogPrefab;

    Dog dog;
    DogController controller;

    PausePanel pausePanel;

    public override void OnStart()
    {
        base.OnStart();
        pausePanel = UIManager.Get.GetPanel<PausePanel>();
    }

    protected override void OnActionPressed(int actionID)
    {
        if (actionID == PlayerActions.Start)
        {
            IsPaused = !IsPaused;
            pausePanel.IsVisible = IsPaused;
        }
    }

    protected override void LoadGame()
    {
        dog = Instantiate(DogPrefab);
        controller = new GameObject().AddComponent<DogController>();
        controller.target = dog;
    }

    protected override void UnloadGame()
    {
        Destroy(dog.gameObject);
        Destroy(controller.gameObject);

        dog = null;
        controller = null;
    }
}