using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Deft.UI;

// Modal Panel returning a string indicating the Control ID of the pressed key.
public class AnyKeyModalPanel : UIModalPanel<string>
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
                string parentName = control.parent.name;
                string fullControlName = control.name;

                if (parentName.Contains("Stick"))
                    fullControlName = parentName + "/" + control.name;

                UIManager.Get.PopModal();
                actionOnPop?.Invoke(fullControlName);
                break;
            }

            yield return null;
        }

    }
}
