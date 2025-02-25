using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float mouseSensitivity = 100f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public Transform cameraHolder;

    private CharacterController characterController;
    private float verticalRotation = 0f;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = characterController.isGrounded;

        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Sprinting
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        characterController.Move(move * currentSpeed * Time.deltaTime);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small force to keep the player grounded
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}