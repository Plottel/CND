﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Deft;

namespace Deft.UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        public delegate void VisibilityChangedHandler(bool visible);
        public VisibilityChangedHandler eventVisibilityChanged;

        [HideInInspector]
        public System.Action actionOnClose;

        [System.NonSerialized] public Canvas canvas;
        [System.NonSerialized] public CanvasGroup canvasGroup;

        private List<Selectable> selectables;

        private void Awake()
        {
            eventVisibilityChanged += OnVisibilityChanged;
            selectables = GetComponentsInChildren<Selectable>().ToList();

            if (gameObject.GetComponent<Selectable>(out var selectable))
                selectables.Add(selectable);
            
            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        private bool isVisible;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    IsInteractable = value;
                    canvas.gameObject.SetActive(value);
                    eventVisibilityChanged?.Invoke(value);
                    actionOnClose?.Invoke();
                    actionOnClose = null;

                    if (isVisible)
                        SelectFirstElement();
                }
            }
        }

        public bool IsInteractable
        {
            get => canvasGroup.interactable;
            set => canvasGroup.interactable = value;
        }

        public void SelectFirstElement()
        {
            if (selectables.Count > 0)
                selectables[0].Select();
        }

        public void Show(System.Action onClose = null)
        {
            IsInteractable = true;
            IsVisible = true;
            actionOnClose = onClose;
        }

        public void Hide()
        {
            IsVisible = false;
            IsInteractable = false;
        }

        public T Find<T>(string name) where T : Object
            => transform.Find<T>(name);

        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }
        protected virtual void OnVisibilityChanged(bool value) { }
    }
}
