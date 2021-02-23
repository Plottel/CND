using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LungeAbility : Ability
{
    public float range = 12f;
    public float speed = 0.5f;

    private Tween lungeTween;

    public override bool IsReady
    {
        get
        {
            float distance = Vector2.Distance(entity.transform.position, targetPosition);
            return base.IsReady && distance <= range;
        }
    }

    protected override void OnUse()
    {
        float distance = Vector2.Distance(entity.transform.position, targetPosition);
        float tweenDuration = speed / distance;
        lungeTween = transform.DOMove(targetPosition, tweenDuration).OnComplete(OnEndUse);

        base.OnUse();
    }

    protected override void OnEndUse()
    {
        lungeTween = null;
        base.OnEndUse();
    }
}
