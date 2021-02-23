using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAbility : Ability
{
    protected override void OnUse()
    {
        SimulationManager.Get.TogglePause();
        base.OnUse();
    }
}
