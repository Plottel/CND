using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : Prop
{
    public float BaseMoveSpeed = 0.02f;

    [HideInInspector] public float moveSpeed;

    private Vector2 heading;
    public Vector2 Heading
    {
        get => heading;
        set => heading = value.normalized;
    }

    protected virtual void Awake()
    {
        moveSpeed = BaseMoveSpeed;
    }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        if (Heading != Vector2.zero)
            transform.Translate(heading * moveSpeed);
    }
}
