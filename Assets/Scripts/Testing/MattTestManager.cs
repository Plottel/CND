using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;

public class MattTestManager : Manager<MattTestManager>
{
    public override void OnStart()
    {
        StartCoroutine(UISequence());
    }

    void PrintOnClose()
    {
        Debug.Log("On Close");
    }

    IEnumerator UISequence()
    {
        UIManager.Get.Show<InputDebugPanel>(PrintOnClose);
        yield return null;
        UIManager.Get.Hide<InputDebugPanel>();
        yield return null;
        UIManager.Get.Show<InputDebugPanel>();
        yield return null;
        UIManager.Get.Hide<InputDebugPanel>();
        yield return null;
        UIManager.Get.Show<InputDebugPanel>(PrintOnClose);
        yield return null;
        UIManager.Get.Hide<InputDebugPanel>();
        yield return null;
        UIManager.Get.Show<InputDebugPanel>();
    }
}
