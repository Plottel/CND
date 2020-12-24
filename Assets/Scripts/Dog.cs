using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Deft;

public class Dog : MonoBehaviour
{
    public float MoveSpeed = 0.02f;

    [HideInInspector] public Vector2 moveHeading;

    private void Update()
    {
        if (moveHeading != Vector2.zero)
            transform.Translate(moveHeading * MoveSpeed);
    }
}
