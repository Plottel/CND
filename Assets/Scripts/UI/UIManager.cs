using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Deft;
using Deft.UI;

public class UIManager : Manager<UIManager>
{
    const string kCloneString = "(Clone)";
    const string kCanvasString = "Canvas-";

    [SerializeField] UIPanelCache panelCache = null;
    [SerializeField] Canvas panelCanvasTemplate = null;
    [SerializeField] Canvas modalOverlay = null;

    private EventSystem eventSystem;

    private List<UIPanel> panels;
    private UIPanel activeModal;

    public override void OnAwake()
    {
        eventSystem = EventSystem.current;
        panels = new List<UIPanel>();

        foreach (UIPanel panelPrefab in panelCache.prefabs)
            SpawnPanel(panelPrefab);
    }

    public void Show<T>() where T : UIPanel 
        => GetPanel<T>().IsVisible = true;

    public void Show<T>(string name) where T : UIPanel 
        => GetPanel<T>(name).IsVisible = true;

    public void Show<T>(System.Action onClose = null) where T : UIPanel
        => GetPanel<T>().Show(onClose);

    public void Show<T>(string name, System.Action onClose = null) where T : UIPanel
        => GetPanel<T>(name).Show(onClose);

    public void Hide<T>() where T : UIPanel 
        => GetPanel<T>().IsVisible = false;

    public void Hide<T>(string name) where T : UIPanel 
        => GetPanel<T>(name).IsVisible = false;

    public void Toggle<T>(bool visible) where T : UIPanel
        => GetPanel<T>().IsVisible = visible;

    public void Toggle<T>(bool visible, string name) where T : UIPanel
        => GetPanel<T>(name).IsVisible = visible;

    public void PushModal<TPanel, TReturn>(System.Action<TReturn> onPopCallback)
    {
        foreach (UIPanel panel in panels)
        {
            if (panel.IsVisible)
                panel.IsInteractable = false;
        }

        var modal = GetModal<TPanel, TReturn>();
        modal.actionOnPop = onPopCallback;

        modalOverlay.gameObject.SetActive(true);
        modal.Show();

        activeModal = modal;
    }

    public UIModalPanel<TReturn> GetModal<TPanel, TReturn>()
    {
        foreach (UIPanel panel in panels)
        {
            if (panel is TPanel)
                return panel as UIModalPanel<TReturn>;
        }

        return null;
    }

    public void PopModal()
    {
        modalOverlay.gameObject.SetActive(false);
        activeModal.Hide();
        activeModal = null;

        foreach (UIPanel panel in panels)
        {
            if (panel.IsVisible)
                panel.IsInteractable = true;
        }

        SelectFirstElement();
    }

    void SelectFirstElement()
    {
        foreach (UIPanel panel in panels)
        {
            if (panel.IsVisible && panel.IsInteractable)
                panel.SelectFirstElement();
        }
    }

    public T GetPanel<T>() where T : UIPanel
    {
        foreach (UIPanel panel in panels)
        {
            T childPanel = panel as T;
            if (childPanel != null)
                return childPanel;
        }
        return null;
    }

    public T GetPanel<T>(string name) where T : UIPanel
    {
        foreach (UIPanel panel in panels)
        {
            if (panel.name == name)
                return panel as T;
        }
        return null;
    }

    public void InjectNavigate(MoveDirection direction)
    {
        var eventData = new AxisEventData(eventSystem);
        eventData.moveDir = direction;
        eventData.selectedObject = eventSystem.currentSelectedGameObject;     

        ExecuteEvents.Execute(eventData.selectedObject, eventData, ExecuteEvents.moveHandler);
    }

    public void InjectNavigate(Vector2 direction)
    {
        var eventData = new AxisEventData(eventSystem);
        eventData.moveVector = direction;
        eventData.selectedObject = eventSystem.currentSelectedGameObject;

        ExecuteEvents.Execute(eventData.selectedObject, eventData, ExecuteEvents.moveHandler);
    }

    private void SpawnPanel(UIPanel prefab)
    {
        Canvas newCanvas = Instantiate(panelCanvasTemplate, transform);
        UIPanel newPanel = Instantiate(prefab, newCanvas.transform);

        newPanel.canvas = newCanvas;
        newPanel.canvasGroup = newCanvas.GetComponent<CanvasGroup>();
        newCanvas.gameObject.SetActive(false);

        // Remove "(Clone)" and add "Canvas-" to newPanel name
        newPanel.name = newPanel.name.Replace(kCloneString, string.Empty);
        newCanvas.name = string.Concat(kCanvasString, newPanel.name);

        panels.Add(newPanel);
    }
}
