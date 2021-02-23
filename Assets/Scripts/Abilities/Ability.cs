using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityStyle
{
    Simple,
    PositionTarget
}

public class Ability : MonoBehaviour
{
    public event System.Action eventUse;
    public event System.Action eventEndUse;

    [HideInInspector] public Entity entity;
    [HideInInspector] public Vector3 targetPosition;

    public AbilityStyle style;
    public float Cooldown;
    protected float lastUse;

    public float RemainingCooldown
    {
        get
        {
            if (lastUse == 0f)
                return 0f;
            return Cooldown - (Time.time - lastUse);
        }
    }

    public virtual bool IsReady { get => RemainingCooldown <= 0f; }

    public bool Use()
    {
        if (IsReady)
        {
            OnUse();
            return true;
        }

        return false;
    }

    protected virtual void OnUse()
    {
        lastUse = Time.time;
        eventUse?.Invoke();
    }

    protected virtual void OnEndUse() => eventEndUse?.Invoke();
}
