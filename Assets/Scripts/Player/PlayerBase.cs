using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public abstract class PlayerBase : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private Transform playerCamera;
        [SerializeField] private Animator animator;

        protected Rigidbody PlayerRigidbody => playerRigidbody;
        protected Transform PlayerCamera => playerCamera;
        protected Animator Animator => animator;

        public PlayerDataModel DataModel { get; protected set; }
        public bool isInitialized { get; protected set; }
    }
}
