using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerCombat
{
    public void Initialize()
    {
        InitializeCombat();
    }

    private void Update()
    {
        CameraRotation();
        MoveInput();
        Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }
}
