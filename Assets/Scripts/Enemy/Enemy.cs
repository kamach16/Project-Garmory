using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour, IKillable
    {
        [SerializeField] private float healthPoints;

        [Header("References")]
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem hitVFX;
        [SerializeField] private Collider bodyCollider;

        private float maxHealth;
        private bool isDead = false;

        private Camera mainCamera;

        private void Start()
        {
            healthPoints = Random.Range(50, 100);
            maxHealth = healthPoints;

            mainCamera = Camera.main;
        }

        private void Update()
        {
            RotateToPlayer();
        }

        private void RotateToPlayer()
        {
            if (isDead)
                return;

            Vector3 lookDirection = transform.position - mainCamera.transform.position;
            lookDirection.y = 0;

            transform.rotation = Quaternion.LookRotation(lookDirection);
        }

        public void DealDamage(int damage)
        {
            if (isDead)
                return;

            healthPoints -= damage;

            healthBar.UpdateHealthBar(healthPoints, maxHealth);
            hitVFX.Play();

            if (healthPoints <= 0)
            {
                animator.SetBool("death", true);
                healthBar.gameObject.SetActive(false);
                bodyCollider.enabled = false;

                isDead = true;

            }
        }
    }
}
