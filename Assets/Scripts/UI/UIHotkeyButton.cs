using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Deft;
using TMPro;

public class UIHotkeyButton : Button
{
    private TextMeshProUGUI actionText;
    private TextMeshProUGUI controlText;

    protected override void Awake()
    {
        base.Awake();
        actionText = transform.Find<TextMeshProUGUI>("ActionName");
        controlText = transform.Find<TextMeshProUGUI>("ControlName");
    }

    public string ActionName
    {
        get => actionText.text;
        set => actionText.text = value;
    }

    public string ControlName
    {
        get => controlText.text;
        set => controlText.text = value;
    }
}
