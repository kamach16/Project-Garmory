using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float moveAcceleration;

    [Header("Components")]
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Transform playerCamera;

    private Vector2 currentInput = Vector2.zero;
    private Vector3 moveDirection;

    private float xRotation = 0f;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CameraRotation();
        MoveInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void MoveInput()
    {
        Vector2 targetInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        currentInput = Vector2.Lerp(
            currentInput,
            targetInput,
            moveAcceleration * Time.fixedDeltaTime
        );

        moveDirection = transform.right * currentInput.x + transform.forward * currentInput.y;
    }

    private void Move()
    {
        playerRigidbody.velocity = new Vector3(moveDirection.x * moveSpeed, playerRigidbody.velocity.y, moveDirection.z * moveSpeed);
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
