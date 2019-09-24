using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController :  Pawn
{
    [Space]
    [SerializeField, Range(0.01f, 2f)] private float rotationDeadZoneSize = 0.05f;

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
            SetRunWalkBools(true, false);
        }
        else if (Input.GetAxis("Crouch") > 0f)
        {
            SetRunWalkBools(false, true);
        }
        else
        {
            SetRunWalkBools(false, false);
        }

        // Pass values from the input controller into the animator to generate movement
        anim.SetFloat("Horizontal", localMoveVector.x * moveSpeed);
        anim.SetFloat("Vertical", localMoveVector.z * moveSpeed);

        return localMoveVector;
    }

    public override Vector3 HandleRotation(Camera pawnCamera = null)
    {
        // Create a plane object for the plane the player is standing on
        Plane groundPlane = new Plane(Vector3.up, tf.position);

        // Create a ray from camera through the mouse position in the direction the camera is facing
        if (pawnCamera != null)
        {
            // Uses the mouse position to rotate the pawn
            Ray mouseRay = pawnCamera.ScreenPointToRay(Input.mousePosition);

            // Raycast
            float intersectDistance;
            if (groundPlane.Raycast(mouseRay, out intersectDistance))
            {
                // TODO: Dead zone when mouse is under player's feet
                Vector3 collisionPoint = mouseRay.GetPoint(intersectDistance);

                //if (Vector3.Distance(collisionPoint, tf.position) <= rotationDeadZoneSize)
                //{
                //    return Vector3.negativeInfinity;
                //}

                // Get rotation needed to look at that point
                Quaternion targetRotation = Quaternion.LookRotation(collisionPoint - tf.position, tf.up);
                tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, turnSpeed * Time.deltaTime) ;
                // Vector3 lookVector = (collisionPoint - tf.position).normalized * rotationDeadZoneSize;
                // tf.LookAt(collisionPoint + lookVector);
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
