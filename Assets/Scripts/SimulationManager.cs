using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;

public class SimulationManager : Manager<SimulationManager>
{
    public Dog DogPrefab;

    Dog dog;
    DogController controller;

    public override void OnStart()
    {
        GameManager.Get.eventEnterGame += OnEnterGame;
    }

    private void OnEnterGame()
    {
        dog = Instantiate(DogPrefab);
        controller = new GameObject().AddComponent<DogController>();
        controller.target = dog;
    }
}
