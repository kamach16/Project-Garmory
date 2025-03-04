using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private void Update()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        fillImage.fillAmount = currentHealth / maxHealth;
    }
}
