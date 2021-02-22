using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Deft;

public class Dog : Entity
{
    SprintAbility sprint;
    LungeAbility lunge;

    protected override void Awake()
    {
        base.Awake();

        sprint = GetComponent<SprintAbility>();
        lunge = GetComponent<LungeAbility>();

        sprint.entity = this;
        lunge.entity = this;
    }

    public Ability GetAbility(int index)
    {
        if (index == 1)
            return sprint;
        else if (index == 2)
            return lunge;
        return null;
    }

    public bool UseAbility(int index)
    {
        if (index == 1)
            return sprint.Use();
        else if (index == 2)
            return lunge.Use();
        return false;
    }
}
