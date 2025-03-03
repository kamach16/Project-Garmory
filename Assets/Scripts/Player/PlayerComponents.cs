using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerComponents : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Animator animator;

    protected Rigidbody PlayerRigidbody => playerRigidbody;
    protected Transform PlayerCamera => playerCamera;
    protected Animator Animator => animator;
}
