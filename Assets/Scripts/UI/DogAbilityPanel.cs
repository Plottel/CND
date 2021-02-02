using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Deft;
using Deft.UI;
using Deft.Input;

public class DogAbilityPanel : UIPanel
{
    public DogController controller;

    Button sprintButton;
    Button lungeButton;

    protected override void OnAwake()
    {
        sprintButton = Find<Button>("SprintButton");
        lungeButton = Find<Button>("LungeButton");

        sprintButton.onClick.AddListener(OnSprintButtonClicked);
    }

    void OnSprintButtonClicked()
    {
        if (controller.ActivateSprint())
        {
            Debug.Log("Hey ability actually got used");
        }
    }
}
