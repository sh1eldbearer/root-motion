using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController :  AgentController
{ 
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        StartCoroutine(Move());
        // TODO: When multiplayer is working, change this to point to the appropriate camera
        StartCoroutine(HandleRotation(AgentCamera));
    }

    /// <summary>
    /// Moves the agent, relative to local space.
    /// </summary>
    /// <returns>The vector the player is moving toward, in local space, if the game is not paused.
    /// Returns null otherwise. </returns>
    public IEnumerator Move()
    {
        while (true)
        {
            while (GameManager.gm.IsGameRunning)
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
                AgentAnimator.SetFloat("Horizontal", localMoveVector.x * MoveSpeed);
                AgentAnimator.SetFloat("Vertical", localMoveVector.z * MoveSpeed);

                StartCoroutine(AgentCamera.UpdateCameraPosition());
                yield return localMoveVector;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Rotates the agent to face where the mouse is pointing, relative to local space.
    /// </summary>
    /// <param name="activeCamera">The currently active camera in the game scene.</param>
    /// <returns>The point in world space at which the mouse intersects with the ground plane.
    /// Returns Vector3.negativeInfinity if the camera is not facing the player. Returns null
    /// if the game is currently paused.</returns>
    public override IEnumerator HandleRotation(CameraController activeCamera)
    {
        while (true)
        {
            while (GameManager.gm.IsGameRunning)
            {
                // Create a plane object for the plane the player is standing on
                Plane groundPlane = new Plane(Vector3.up, AgentTransform.position);

                // Create a ray from camera through the mouse position in the direction the camera is facing
                if (activeCamera != null)
                {
                    // Uses the mouse position to rotate the pawn
                    Ray mouseRay = activeCamera.ThisCamera.ScreenPointToRay(Input.mousePosition);

                    // Raycast
                    float intersectDistance;
                    if (groundPlane.Raycast(mouseRay, out intersectDistance))
                    {
                        // Gets the point in world space where the mouse is aiming
                        Vector3 collisionPoint = mouseRay.GetPoint(intersectDistance);

                        // Get the rotation needed for the player to look at that point
                        Quaternion targetRotation =
                            Quaternion.LookRotation(collisionPoint - AgentTransform.position, AgentTransform.up);
                        AgentTransform.rotation = Quaternion.RotateTowards(AgentTransform.rotation, targetRotation,
                            TurnSpeed * Time.deltaTime);
                        yield return collisionPoint;
                    }
                    else
                    {
                        Debug.LogError("Camera is not looking at plane");
                        yield return Vector3.negativeInfinity;
                    }
                }
            }

            yield return null;
        }
    }
}
