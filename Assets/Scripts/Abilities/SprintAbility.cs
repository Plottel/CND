using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAbility : Ability
{
    public float speedMultiplier = 2.5f;
    public float duration = 3f;

    protected override void OnUse()
    {
        entity.moveSpeed += entity.BaseMoveSpeed * speedMultiplier;
        base.OnUse();

        StartCoroutine(DeactiveSprintAfterDuration());
    }

    IEnumerator DeactiveSprintAfterDuration()
    {
        while (Time.time - lastUse < duration)
            yield return null;

        entity.moveSpeed -= entity.BaseMoveSpeed * speedMultiplier;
        base.OnEndUse();
    }
}
