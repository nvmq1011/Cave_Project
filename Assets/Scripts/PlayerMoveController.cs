using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    [Header("Recoil")]
    public float recoilAmount = 20f;
    public float recoilSpeed = 60f;
    public float recoilReturnSpeed = 4f;

    private float gravity = -9.81f;
    private float yVelocity;

    private float xRotation = 0f;
    private CharacterController controller;

    // Recoil
    private float recoilOffset;
    private float recoilTarget;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Hạ recoil dần về 0
        recoilTarget = Mathf.Lerp(
            recoilTarget,
            0f,
            recoilReturnSpeed * Time.deltaTime);

        Look();
        Move();
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Nhìn lên xuống
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Recoil mượt
        recoilOffset = Mathf.Lerp(
            recoilOffset,
            recoilTarget,
            recoilSpeed * Time.deltaTime);

        playerCamera.localRotation =
            Quaternion.Euler(xRotation + recoilOffset, 0f, 0f);

        // Nhìn trái phải
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

    public void RecoilFire()
    {
        recoilTarget -= recoilAmount;
    }
}