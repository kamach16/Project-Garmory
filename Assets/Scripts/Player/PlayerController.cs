using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : PlayerCombat
    {
        public void Initialize(PlayerDataModel dataModel)
        {
            this.DataModel = dataModel;

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
}
