using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Deft;

public class Dog : Prop
{
    // TODO(Matt): Pull these out into "DogData / DogStats" Scriptable Object?
    public float BaseMoveSpeed = 0.02f;

    // TODO(Matt/Cory): Devise mathematical system for consistent stat manipulation
    public float SprintMultiplier = 1.5f;
    public float SprintDuration = 3f;
    public float SprintCooldown = 10f;

    private Vector2 heading;
    public Vector2 Heading 
    {
        get => heading;
        set => heading = value.normalized;
    }

    private float moveSpeed;
    private float lastSprintActivation;

    private void Awake()
    {
        moveSpeed = BaseMoveSpeed;
    }

    private void Update()
    {
        if (Heading != Vector2.zero)
            transform.Translate(heading * moveSpeed);
    }

    public bool ActivateSprint()
    {
        if (TimeSince(lastSprintActivation) > SprintCooldown || lastSprintActivation == 0f)
        {
            lastSprintActivation = Time.time;
            moveSpeed *= SprintMultiplier;
            StartCoroutine(DeactiveSprintAfterDuration());
            return true;
        }

        return false;
    }

    // TODO(Matt): Investigate coroutines on destroyed objects - OnDestroy->StopAllCoroutines?
    IEnumerator DeactiveSprintAfterDuration()
    {
        while (TimeSince(lastSprintActivation) < SprintDuration)
            yield return null;

        moveSpeed /= SprintMultiplier;
    }

    float TimeSince(float value) => Time.time - value;
}
