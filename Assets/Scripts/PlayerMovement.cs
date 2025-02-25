using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerMovement : MonoBehaviour
{
    public EventReference footstepEvent; // Assign the FMOD Event path in the Inspector
    // Use EventReference instead of [EventRef]

    private FMOD.Studio.EventInstance footstepEventInstance;

    private bool isMoving = false;
    private Vector3 lastPosition;

    private void Start()
    {
        // Create an instance of the footstep event
        footstepEventInstance = RuntimeManager.CreateInstance(footstepEvent);
        footstepEventInstance.start();

        lastPosition = transform.position;
    }

    private void Update()
    {
        // Check if the player is moving
        if (transform.position != lastPosition)
        {
            if (!isMoving)
            {
                isMoving = true;
                SetMovementParameter(1); // Set IsMoving to 1 (true)
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                SetMovementParameter(0); // Set IsMoving to 0 (false)
            }
        }

        lastPosition = transform.position;
    }

    // Update the FMOD parameter
    private void SetMovementParameter(int value)
    {
        footstepEventInstance.setParameterByName("IsMoving", value);
    }

    private void OnDestroy()
    {
        // Stop and release the event when the GameObject is destroyed
        footstepEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        footstepEventInstance.release();
    }
}
