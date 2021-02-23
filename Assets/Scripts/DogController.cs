using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;

public class DogController : MonoBehaviour
{
    public delegate void ChooseTargetChangedHandler(bool value, int abilityIndex);
    public event ChooseTargetChangedHandler eventIsChoosingTargetChanged;

    public Dog target { get; private set; }

    MoveAbility movement;
    SprintAbility sprint;
    LungeAbility lunge;
    PauseAbility pause;

    int abilityChoice;
    bool isChoosingTarget;
    public bool IsChoosingTarget 
    {
        get => isChoosingTarget;
        private set
        {
            if (value != isChoosingTarget)
            {
                isChoosingTarget = value;
                eventIsChoosingTargetChanged?.Invoke(isChoosingTarget, abilityChoice);
            }
        }
    }

    public void SetTarget(Dog newTarget)
    {
        target = newTarget;

        if (target == null)
            return;

        movement = target.GetAbility<MoveAbility>(PlayerActions.Movement);
        sprint = target.GetAbility<SprintAbility>(PlayerActions.Primary);
        lunge = target.GetAbility<LungeAbility>(PlayerActions.Secondary);
        pause = target.GetAbility<PauseAbility>(PlayerActions.Start);
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

    public bool TryUseAbility(int abilityIndex) => target.TryUseAbility(abilityIndex);

    public bool BeginChoosingTarget(int abilityIndex)
    {
        abilityChoice = abilityIndex;
        IsChoosingTarget = target.GetAbility(abilityIndex).IsReady;
        return IsChoosingTarget;
    }

    void OnAxisMoved(int actionID, Vector2 axis)
    {
        if (actionID == PlayerActions.Movement)
        {
            movement.direction = axis;
            target.TryUseAbility(PlayerActions.Movement);
        }
    }

    void OnActionPressed(int actionID)
    {
        switch (target.GetAbility(actionID).style)
        {
            case AbilityStyle.Simple:
                target.TryUseAbility(actionID);
                break;

            case AbilityStyle.PositionTarget:
                BeginChoosingTarget(actionID);
                break;
        }
    }

    void OnMousePressed(int buttonID, Vector2 mousePos)
    {
        if (buttonID == 0)
        {
            if (isChoosingTarget && target.TryUseAbility(abilityChoice))
                isChoosingTarget = false;
        }
    }
}
