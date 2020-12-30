using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deft.UI
{
    public class UIModalPanel<T> : UIPanel
    {
        public System.Action<T> actionOnPop;
    }
}

