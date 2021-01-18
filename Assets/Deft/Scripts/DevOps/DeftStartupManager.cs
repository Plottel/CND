using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Deft.DevOps
{
    public class DeftStartupManager : Manager<DeftStartupManager>
    {
        protected EventSystem eventSystem;

        public override void OnStart()
        {
            UIManager.Get.Show<StartupPanel>();
        }
    }
}
