using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleMessage : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;

    public void SetText(string message)
    {
        text.text = message;
    }
}
