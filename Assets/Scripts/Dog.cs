using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Deft;

public class Dog : Entity
{
    Ability[] abilities;

    protected override void Awake()
    {
        base.Awake();

        abilities = new Ability[PlayerActions.Count];
        abilities[PlayerActions.Movement] = GetComponent<MoveAbility>();
        abilities[PlayerActions.Primary] = GetComponent<SprintAbility>();
        abilities[PlayerActions.Secondary] = GetComponent<LungeAbility>();
        abilities[PlayerActions.Start] = GetComponent<PauseAbility>();

        foreach (var ability in abilities)
            ability.entity = this;
    }

    public Ability GetAbility(int index) => abilities[index];
    public T GetAbility<T>(int index) where T : Ability => abilities[index] as T;

    public bool TryUseAbility(int index) => abilities[index].Use();
}
