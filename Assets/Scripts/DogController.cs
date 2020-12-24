using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public Dog target;

    private void Awake()
    {
        InputManager.Get.eventAxisMoved += OnAxisMoved;
    }

    private void OnDestroy()
    {
        InputManager.Get.eventAxisMoved -= OnAxisMoved;
    }

    void SetDogHeading(Vector2 direction) => target.moveHeading = direction.normalized;

    void OnAxisMoved(int actionID, Vector2 axis)
    {
        if (actionID == PlayerActions.Movement)
            SetDogHeading(axis);
    }
}
