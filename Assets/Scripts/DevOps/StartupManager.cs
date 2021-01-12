using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;
using Deft.DevOps;
using UnityEngine.EventSystems;

public class StartupManager : DeftStartupManager
{
    IEnumerator HotkeysPanelRoutine()
    {
        UIManager.Get.Show<HotkeysPanel>();

        for (int i = 0; i < 5; ++i)
        {
            yield return new WaitForSeconds(1);
            UIManager.Get.InjectNavigate(MoveDirection.Down);
        }
    }
}
