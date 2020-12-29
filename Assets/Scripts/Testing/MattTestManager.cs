using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Deft;

public class MattTestManager : Manager<MattTestManager>
{
    InputDebugPanel inputPanel;

    public override void OnStart()
    {
        UIManager.Get.Show<InputDebugPanel>();
    }
}
