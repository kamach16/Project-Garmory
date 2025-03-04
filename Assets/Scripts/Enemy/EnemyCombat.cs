using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour, IKillable
{
    [SerializeField] private int healthPoints;

    private void Start()
    {
        healthPoints = Random.Range(50, 100);
    }

    public void DealDamage(int damage)
    {
        healthPoints -= damage;
        
        if (healthPoints <= 0)
            Destroy(gameObject);
    }
}
