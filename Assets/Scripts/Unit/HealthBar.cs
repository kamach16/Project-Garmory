using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Unit
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            LookAtPlayer();
        }

        private void LookAtPlayer()
        {
            transform.LookAt(2 * transform.position - mainCamera.transform.position);
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            fillImage.fillAmount = currentHealth / maxHealth;
        }
    }
}
