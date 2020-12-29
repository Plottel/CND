using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Deft;
using Deft.UI;

public class MattTestPanel : UIPanel
{
    protected override void OnVisibilityChanged(bool value)
    {
        base.OnVisibilityChanged(value);

        if (value)
            StartCoroutine(ListenForKeyPress());
    }

    IEnumerator ListenForKeyPress()
    {
        while (true)
        {
            var reader = InputManager.Get.GetActiveReader<PlayerInputReader>();
            if (reader.AnyControlPressed(out InputControl control))
            {
                Debug.Log(control.name);
                UIManager.Get.PopModal();
                break;
            }

            yield return null;
        }

    }
}
