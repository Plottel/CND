using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;

public class MattTestManager : Manager<MattTestManager>
{
    public override void OnStart()
    {
        UIManager.Get.Show<InputDebugPanel>();
    }
}
