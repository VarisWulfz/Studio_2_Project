using FMODUnity;
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
    private bool isMoving = false;
    private Vector3 lastPosition;
    private bool isSprinting = false; // Track if the player is sprinting

    public FMODUnity.StudioEventEmitter moveSoundEmit; // Footstep emitter
    public FMODUnity.StudioEventEmitter sprintSoundEmit; // Sprint sound emitter

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
                
        /*
        // Old move and sprint sound emit
        //Check if the player is moving
        if (move.magnitude > 0.1f && isGrounded)
        {
            //it is a loop sound, so if I'm NOT playing it, play it.
            if (moveSoundEmit.IsPlaying() == false)
            {
                //Debug.Log("PLAY " + mag);
                moveSoundEmit.Play();
            }

            //otherwise I would thread flood the object with start instructions

        }
        else
        {
            //it is a loop sound, so if I AM playing it, stop  it.
            if (moveSoundEmit.IsPlaying() == true)
            {
                //Debug.Log("STOP " + mag);
                moveSoundEmit.Stop();
            }

            //otherwise I would thread flood the object with stop instructions
        }

        // Sprinting
        float currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;

            // Start sprint sound if not already playing
            if (!isSprinting)
            {
                isSprinting = true;
                sprintSoundEmit.Play(); // Play sprint sound
            }
        }
        else
        {
            // Stop sprint sound if sprinting ends
            if (isSprinting)
            {
                isSprinting = false;
                sprintSoundEmit.Stop(); // Stop sprint sound
            }
        }
        */

        // Check if the player is moving
        if (move.magnitude > 0.1f && isGrounded)
        {
            // If sprinting, stop the normal footstep sound and play the sprint sound
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!isSprinting)
                {
                    isSprinting = true;
                    if (moveSoundEmit.IsPlaying()) moveSoundEmit.Stop(); // Stop footstep sound
                    sprintSoundEmit.Play(); // Play sprint sound
                }
            }
            else // Not sprinting
            {
                if (isSprinting)
                {
                    isSprinting = false;
                    if (sprintSoundEmit.IsPlaying()) sprintSoundEmit.Stop(); // Stop sprint sound
                }

                // Play footstep sound if not already playing
                if (!moveSoundEmit.IsPlaying())
                {
                    moveSoundEmit.Play();
                }
            }
        }
        else // Player is not moving
        {
            // Stop both footstep and sprint sounds
            if (moveSoundEmit.IsPlaying()) moveSoundEmit.Stop();
            if (sprintSoundEmit.IsPlaying()) sprintSoundEmit.Stop();
            isSprinting = false;
        }

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