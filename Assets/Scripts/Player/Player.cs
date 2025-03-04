using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerCombat
{
    public void Initialize(PlayerDataModel DataModel)
    {
        this.DataModel = DataModel;

        isInitialized = true;

        InitializeCombat();
        InitializeMovement();
    }

    private void Update()
    {
        CameraRotation();
        MoveInput();
        Attack();
        CheckIsGrounded();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }
}
