using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private Transform playerCamera;

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
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
}
