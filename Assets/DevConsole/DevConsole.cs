using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevConsole : MonoBehaviour
{
    public static DevConsole dc;
    public GameObject scrollContent;
    public GameObject MessagePrefab;

    private List<GameObject> messages = new List<GameObject>();

    void Start()
    {
        dc = this;
    }

    public void PostMessage(string message, GameObject sender = null, Time timeStamp = null)
    {
        GameObject messageInstance = Instantiate(MessagePrefab, scrollContent.transform);
        var newMessageRect = messageInstance.GetComponent<RectTransform>();
        float posY = -20;
        float posX = newMessageRect.anchoredPosition.x;

        if (messages.Count > 0)
        {
            var lastMessageRect = messages[messages.Count - 1].GetComponent<RectTransform>();
            posY = lastMessageRect.anchoredPosition.y - 40;
        }
        newMessageRect.anchoredPosition = new Vector2(posX, posY);
        messages.Add(messageInstance);
        messageInstance.GetComponent<ConsoleMessage>().SetText(message);

        var scrollRect = scrollContent.GetComponent<RectTransform>();
        var size = new Vector2(scrollRect.sizeDelta.x, scrollRect.sizeDelta.y + newMessageRect.rect.height);
        scrollRect.sizeDelta = size;
    }

    public void ClearMessages()
    {
        foreach (var messageInstance in messages)
        {
            Destroy(messageInstance);
        }
        messages.Clear();
        var scrollRect = scrollContent.GetComponent<RectTransform>();
        scrollRect.sizeDelta = new Vector2(scrollRect.sizeDelta.x, 0);
    }
}
