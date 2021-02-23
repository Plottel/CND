using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAbility : Ability
{
    [HideInInspector] public Vector2 direction;

    protected override void OnUse()
    {
        entity.Heading = direction;
        base.OnUse();
    }
}
