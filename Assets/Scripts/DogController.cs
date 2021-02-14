using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;

public class DogController : MonoBehaviour
{
    public delegate void ChooseTargetChangedHandler(bool value);
    public event ChooseTargetChangedHandler eventIsChoosingTargetChanged;

    public Dog target;

    bool isChoosingTarget;
    public bool IsChoosingTarget 
    {
        get => isChoosingTarget;
        private set
        {
            if (value != isChoosingTarget)
            {
                isChoosingTarget = value;
                eventIsChoosingTargetChanged?.Invoke(isChoosingTarget);
            }
        }
    }

    private void Awake()
    {
        InputManager.Get.eventAxisMoved += OnAxisMoved;
        InputManager.Get.eventActionPressed += OnActionPressed;
        InputManager.Get.eventMousePressed += OnMousePressed;
    }

    private void OnDestroy()
    {
        InputManager.Get.eventAxisMoved -= OnAxisMoved;
        InputManager.Get.eventActionPressed -= OnActionPressed;
        InputManager.Get.eventMousePressed -= OnMousePressed;
    }

    public bool ActivateSprint() => target.ActivateSprint();
    public bool ActivateLunge(Vector3 target) => this.target.ActivateLunge(target);

    public bool CanActivateSprint() => target.CanActivateSprint();
    public bool CanActivateLunge() => target.CanActivateLunge();

    public bool BeginChoosingTarget()
    {
        IsChoosingTarget = CanActivateLunge();
        return IsChoosingTarget;
    }

    void OnAxisMoved(int actionID, Vector2 axis)
    {
        if (actionID == PlayerActions.Movement)
            target.Heading = axis;
    }

    void OnActionPressed(int actionID)
    {
        if (actionID == PlayerActions.Primary)
            ActivateSprint();
        else if (actionID == PlayerActions.Secondary)
            BeginChoosingTarget();
    }

    void OnMousePressed(int buttonID, Vector2 mousePos)
    {
        if (buttonID == 0)
        {
            if (isChoosingTarget && ActivateLunge(mousePos))
                isChoosingTarget = false;
        }
    }
}
