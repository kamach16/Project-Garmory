using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovement : PlayerComponents
{
    [Header("Moving")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float moveAcceleration;

    private Vector2 currentInput = Vector2.zero;
    private Vector3 moveDirection;

    private float xRotation = 0f;

    protected void InitializeMovement()
    {
        
    }

    protected void MoveInput()
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

    protected void Move()
    {
        PlayerRigidbody.velocity = new Vector3(moveDirection.x * moveSpeed, PlayerRigidbody.velocity.y, moveDirection.z * moveSpeed);
    }

    protected void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        PlayerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
