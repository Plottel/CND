using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandle : EventTrigger
{
    private bool dragging;
    private RectTransform panel;
    private RectTransform rect;

    public void Start()
    {
        panel = transform.parent.parent.GetComponent<RectTransform>();
        rect = GetComponent<RectTransform>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        DevConsole.dc.PostMessage("Start Drag");
        dragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        DevConsole.dc.PostMessage("End Drag");
        dragging = false;
    }

    void Update()
    {
        if (dragging)
        {
            float xPos = Input.mousePosition.x - panel.sizeDelta.x / 2 + rect.sizeDelta.x / 2;
            float yPos = Input.mousePosition.y - panel.sizeDelta.y / 2 + rect.sizeDelta.y / 2;
            panel.anchoredPosition = new Vector2(xPos, yPos);
        }
    }
}
