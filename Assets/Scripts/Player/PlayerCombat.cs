using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;

    [Header("Components")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Animator animator;
    
    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1") && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) // this condition prevents from override trigger event
            animator.SetTrigger("attack");
    }

    // animation event
    public void PerformAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.TransformDirection(Vector3.forward), out hit, attackRange))
        {
            IKillable killable = hit.transform.GetComponent<IKillable>();

            if (killable != null)
                killable.DealDamage(damage);
        }
    }
}
