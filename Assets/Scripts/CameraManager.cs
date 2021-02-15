using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Deft;

public class CameraManager : Manager<CameraManager>
{
    [SerializeField] private Camera mainCamera;

    public Camera MainCamera { get => mainCamera; } 
}
