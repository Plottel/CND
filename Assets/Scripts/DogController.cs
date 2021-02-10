using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;

public class DogController : MonoBehaviour
{
    public Dog target;

    private void Awake()
    {
        InputManager.Get.eventAxisMoved += OnAxisMoved;
        InputManager.Get.eventActionPressed += OnActionPressed;
    }

    private void OnDestroy()
    {
        InputManager.Get.eventAxisMoved -= OnAxisMoved;
        InputManager.Get.eventActionPressed -= OnActionPressed;
    }

    public bool ActivateSprint() => target.ActivateSprint();
    public bool ActivateLunge(Vector3 target) => this.target.ActivateLunge(target);

    public bool CanActivateSprint() => target.CanActivateSprint();
    public bool CanActivateLunge() => target.CanActivateLunge();

    void OnAxisMoved(int actionID, Vector2 axis)
    {
        if (actionID == PlayerActions.Movement)
            target.Heading = axis;
    }

    void OnActionPressed(int actionID)
    {
        if (actionID == PlayerActions.Primary)
            ActivateSprint();
    }
}
