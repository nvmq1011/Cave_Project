using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    float gravity = -9.81f;
    float yVelocity;

    float xRotation = 0f;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked; // khóa chuột
    }

    void Update()
    {
        Look();
        Move();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Xoay lên xuống (Camera)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Xoay trái phải (Player)
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        yVelocity += gravity * Time.deltaTime;

        move.y = yVelocity;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
