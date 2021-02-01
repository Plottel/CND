using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Deft;
using Deft.UI;

public class PausePanel : UIPanel
{
    Button quitButton;

    protected override void OnAwake()
    {
        quitButton = Find<Button>("QuitButton");
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    void OnQuitButtonClicked()
        => DeftSimulationManager.Get.ExitGame();
}
