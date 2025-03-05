using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovement : PlayerBase
{
    [Header("Moving")]
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float moveAcceleration;
    [SerializeField] private float moveSpeed;

    private Vector2 currentInput = Vector2.zero;
    private Vector3 moveDirection;

    private float xRotation = 0f;
    private bool isGrounded;

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

        moveDirection = (transform.right * currentInput.x + transform.forward * currentInput.y).normalized;
    }

    protected void Move()
    {
        if (!isInitialized)
            return;

        PlayerRigidbody.velocity = new Vector3(moveDirection.x * moveSpeed, PlayerRigidbody.velocity.y, moveDirection.z * moveSpeed);
    }

    protected void CheckIsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 2.5f))
            isGrounded = true;
        else
            isGrounded = false;
    }

    protected void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlayerRigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
            isGrounded = false;
        }
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
