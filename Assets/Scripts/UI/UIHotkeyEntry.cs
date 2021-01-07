using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;
using TMPro;

public class UIHotkeyEntry : MonoBehaviour
{
    private TextMeshProUGUI actionText;
    private TextMeshProUGUI controlText;

    private void Awake()
    {
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
