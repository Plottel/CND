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
#if UNITY_EDITOR
        [MenuItem("GameObject/UI/UIButton")]
        private static void CreateInHierarchy()
        {
            var button = new GameObject();
            button.name = "New UIButton";
            button.AddComponent<Image>();
            button.AddComponent<UIButton>();
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 30);            

            var label = new GameObject().AddComponent<TextMeshProUGUI>();
            label.gameObject.name = "Label";
            label.fontSize = 20;
            label.text = "Text";
            label.transform.SetParent(button.transform);

        }
#endif

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
