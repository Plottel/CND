using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Deft;

public class Dog : Prop
{
    public event System.Action eventSprintStart;
    public event System.Action eventSprintEnd;
    public event System.Action eventLungeStart;
    public event System.Action eventLungeEnd;

    // TODO(Matt): Pull these out into "DogData / DogStats" Scriptable Object?
    public float BaseMoveSpeed = 0.02f;

    [Header("Sprint")]
    // TODO(Matt/Cory): Devise mathematical system for consistent stat manipulation
    public float SprintMultiplier = 2.5f;
    public float SprintDuration = 3f;
    public float SprintCooldown = 6f;
    private float lastSprintActivation;

    [Header("Lunge")]
    public float LungeRange = 12f;
    public float LungeSpeed = 0.5f;
    public float LungeCooldown = 5f;
    private Tween lungeTween;
    private float lastLungeActivation;

    private Vector2 heading;
    public Vector2 Heading 
    {
        get => heading;
        set => heading = value.normalized;
    }

    private float moveSpeed;

    private void Awake()
    {
        moveSpeed = BaseMoveSpeed;
    }

    private void Update()
    {
        if (Heading != Vector2.zero)
            transform.Translate(heading * moveSpeed);
    }

    public bool CanActivateLunge() => TimeSince(lastLungeActivation) > LungeCooldown || lastLungeActivation == 0f;
    public bool CanActivateSprint() => TimeSince(lastSprintActivation) > SprintCooldown || lastSprintActivation == 0f;

    public bool ActivateSprint()
    {
        if (TimeSince(lastSprintActivation) > SprintCooldown || lastSprintActivation == 0f)
        {
            lastSprintActivation = Time.time;
            moveSpeed *= SprintMultiplier;

            eventSprintStart?.Invoke();
            StartCoroutine(DeactiveSprintAfterDuration());
            return true;
        }

        return false;
    }    

    public bool ActivateLunge(Vector3 target)
    {
        float distance = Vector2.Distance(transform.position, target);

        if (distance > LungeRange)
            return false;

        if (TimeSince(lastLungeActivation) > LungeCooldown || lastLungeActivation == 0f)
        {
            lastLungeActivation = Time.time;

            float tweenDuration = LungeSpeed / distance; 
            lungeTween = transform.DOMove(target, tweenDuration).OnComplete(OnLungeEnd);

            eventLungeStart?.Invoke();
            return true;
        }

        return false;
    }

    void OnLungeEnd()
    {
        lungeTween = null;
        eventLungeEnd?.Invoke();
    }

    // TODO(Matt): Investigate coroutines on destroyed objects - OnDestroy->StopAllCoroutines?
    // Is this over-engineering?
    IEnumerator DeactiveSprintAfterDuration()
    {
        while (TimeSince(lastSprintActivation) < SprintDuration)
            yield return null;

        moveSpeed /= SprintMultiplier;
        eventSprintEnd?.Invoke();
    }

    float TimeSince(float value) => Time.time - value;
}
