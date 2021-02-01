using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Deft.UI
{
    public class UIButton : Button
    {
        static readonly ColorBlock defaultColors = new ColorBlock
        {
            normalColor = new Color32(255, 255, 255, 0),
            highlightedColor = new Color32(207, 207, 207, 255),
            pressedColor = new Color32(108, 108, 108, 255),
            selectedColor = new Color32(153, 153, 153, 255),
            disabledColor = new Color32(255, 255, 255, 0),
            colorMultiplier = 1,
            fadeDuration = 0.1f
        };

        protected override void Reset()
        {
            base.Reset();
            colors = defaultColors;
        }
    }
}
