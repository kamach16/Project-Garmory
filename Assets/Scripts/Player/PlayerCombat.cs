using Core;
using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Player
{
    public abstract class PlayerCombat : PlayerMovement
    {
        [Header("Attacking")]
        [SerializeField] private float attackRange;

        protected void InitializeCombat()
        {

        }

        protected void Attack()
        {
            if (GameManager.Instance.IsAtThisGameState(GameState.Paused))
                return;

            if (Input.GetButtonDown("Fire1") &&
                !Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) // this condition prevents from override trigger
                Animator.SetTrigger("attack");
        }

        protected void PerformAttack() // animation event
        {
            RaycastHit hit;
            if (Physics.Raycast(PlayerCamera.position, PlayerCamera.TransformDirection(Vector3.forward), out hit, attackRange))
            {
                IKillable killable = hit.transform.GetComponent<IKillable>();

                if (killable != null)
                    killable.DealDamage(DataModel.Damage);
            }
        }
    }
}