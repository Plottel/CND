using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Deft.DevOps
{
    public class DeftStartupManager : Manager<DeftStartupManager>
    {
        protected EventSystem eventSystem;

#if UNITY_EDITOR
        public void BeginQuickStart(string routineName)
        {
            eventSystem = EventSystem.current;
            if (!string.IsNullOrEmpty(routineName))
                StartCoroutine(routineName);
        }
#endif
    }

}
