using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;

public class DogController : MonoBehaviour
{
    public delegate void ChooseTargetChangedHandler(bool value, int abilityIndex);
    public event ChooseTargetChangedHandler eventIsChoosingTargetChanged;

    public Dog target;

    int targetAbilityIndex;
    bool isChoosingTarget;
    public bool IsChoosingTarget 
    {
        get => isChoosingTarget;
        private set
        {
            if (value != isChoosingTarget)
            {
                isChoosingTarget = value;
                eventIsChoosingTargetChanged?.Invoke(isChoosingTarget, targetAbilityIndex);
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

    public bool UseAbility(int abilityIndex) => target.UseAbility(abilityIndex);
    public bool UseAbility(int abilityIndex, Vector3 targetPosition)
    {
        target.GetAbility(abilityIndex).targetPosition = targetPosition;
        return target.UseAbility(abilityIndex);
    }

    public bool BeginChoosingTarget(int abilityIndex)
    {
        targetAbilityIndex = abilityIndex;
        IsChoosingTarget = target.GetAbility(abilityIndex).IsReady;
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
            target.UseAbility(PlayerActions.Primary);
        else if (actionID == PlayerActions.Secondary)
            BeginChoosingTarget(PlayerActions.Secondary);
        else if (actionID == PlayerActions.Start)
            SimulationManager.Get.TogglePause();
    }

    void OnMousePressed(int buttonID, Vector2 mousePos)
    {
        if (buttonID == 0)
        {
            if (isChoosingTarget && UseAbility(targetAbilityIndex))
                isChoosingTarget = false;
        }
    }
}
