using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerBaseData")]
public class PlayerBaseData : ScriptableObject
{
    [Header("Base Player Stats")]
    [SerializeField] private int damage;
    [SerializeField] private int healthPoints;
    [SerializeField] private int defense;
    [SerializeField] private float lifeSteal;
    [SerializeField] private float criticalStrikeChance;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float luck;

    public int Damage => damage;
    public int HealthPoints => healthPoints;
    public int Defense => defense;
    public float LifeSteal => lifeSteal;
    public float CriticalStrikeChance => criticalStrikeChance;
    public float AttackSpeed => attackSpeed;
    public float MovementSpeed => movementSpeed;
    public float Luck => luck;
}
