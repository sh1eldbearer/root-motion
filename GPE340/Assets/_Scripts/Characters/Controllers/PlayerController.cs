using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]

public class PlayerController :  AgentController
{ 
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        Move();
        HandleRotation(pawnCamera);
    }

    public override Vector3 Move()
    {

        // Get the world vector that we want to move
        Vector3 worldMoveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Normalize it to allow controllers and keyboards to function the same
        worldMoveVector.Normalize();

        // Find local version of the worldMoveVector (relative to the object's transform)
        Vector3 localMoveVector = transform.InverseTransformDirection(worldMoveVector);

        // Set sprinting or walking booleans if any of the inputs are pressed
        if (Input.GetAxis("Sprint") > 0f)
        {
            ChangeLocomotionState(LocomotionState.Sprinting);
        }
        else if (Input.GetAxis("Crouch") > 0f)
        {
            ChangeLocomotionState(LocomotionState.Crouching);
        }
        else
        {
            ChangeLocomotionState(LocomotionState.Walking);
        }

        // Pass values from the input controller into the animator to generate movement
        anim.SetFloat("Horizontal", localMoveVector.x * moveSpeed);
        anim.SetFloat("Vertical", localMoveVector.z * moveSpeed);

        return localMoveVector;
    }

    public override Vector3 HandleRotation(Camera activeCamera = null)
    {
        // Create a plane object for the plane the player is standing on
        Plane groundPlane = new Plane(Vector3.up, tf.position);

        // Create a ray from camera through the mouse position in the direction the camera is facing
        if (activeCamera != null)
        {
            // Uses the mouse position to rotate the pawn
            Ray mouseRay = activeCamera.ScreenPointToRay(Input.mousePosition);

            // Raycast
            float intersectDistance;
            if (groundPlane.Raycast(mouseRay, out intersectDistance))
            {
                // Gets the point in world space where the mouse is aiming
                Vector3 collisionPoint = mouseRay.GetPoint(intersectDistance);

                // Get the rotation needed for the player to look at that point
                Quaternion targetRotation = Quaternion.LookRotation(collisionPoint - tf.position, tf.up);
                tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, turnSpeed * Time.deltaTime);
                return collisionPoint;
            }
            else
            {
                Debug.LogError("Camera is not looking at plane");
                return Vector3.negativeInfinity;
            }
        }
        else
        {
            // TODO: For AI movement (not yet implemented)
            return Vector3.negativeInfinity;
        }
    }
}
