using System.Collections.Generic;
using UnityEngine;
using Deft;
using Deft.UI;

public class UIManager : Manager<UIManager>
{
    const string kCloneString = "(Clone)";
    const string kCanvasString = "Canvas-";

    [SerializeField] UIPanelCache panelCache = null;
    [SerializeField] Canvas panelCanvasTemplate = null;

    private List<UIPanel> panels;

    public override void OnAwake()
    {
        panels = new List<UIPanel>();

        foreach (UIPanel panelPrefab in panelCache.prefabs)
            SpawnPanel(panelPrefab);
    }

    private void SpawnPanel(UIPanel prefab)
    {
        Canvas newCanvas = Instantiate(panelCanvasTemplate, transform);
        UIPanel newPanel = Instantiate(prefab, newCanvas.transform);

        newPanel.canvas = newCanvas;
        newCanvas.gameObject.SetActive(false);

        // Remove "(Clone)" and add "Canvas-" to newPanel name
        newPanel.name = newPanel.name.Replace(kCloneString, string.Empty);
        newCanvas.name = string.Concat(kCanvasString, newPanel.name);

        panels.Add(newPanel);
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
}
