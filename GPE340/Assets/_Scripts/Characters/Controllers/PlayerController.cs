using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : AgentController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        StartCoroutine(Move());
        // TODO: When multiplayer is working, change this to point to the appropriate camera
        StartCoroutine(HandleRotation(ThisPawn.PawnData.PawnCamera));
    }

    /// <summary>
    /// Moves the agent, relative to local space.
    /// </summary>
    /// <returns>A co-routine enumerator.</returns>
    protected override IEnumerator Move()
    {
        while (true)
        {
            while (GameManager.gm.IsGameRunning)
            {
                // Set sprinting or walking booleans if any of the inputs are pressed
                if (Input.GetAxis("Sprint") > 0f)
                {
                    SetLocomotionState(LocomotionState.Sprinting);
                }
                else if (Input.GetAxis("Crouch") > 0f)
                {
                    SetLocomotionState(LocomotionState.Crouching);
                }
                else
                {
                    SetLocomotionState(LocomotionState.Walking);
                }

                ThisPawn.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

                yield return new WaitForFixedUpdate();
                StartCoroutine(ThisPawn.PawnData.PawnCamera.UpdateCameraPosition());
            }

            yield return new WaitForFixedUpdate();
        }
    }

    /// <summary>
    /// Rotates the agent to face where the mouse is pointing, relative to local space.
    /// </summary>
    /// <param name="activeCamera">The currently active camera in the game scene.</param>
    /// <returns>The point in world space at which the mouse intersects with the ground plane.
    /// Returns Vector3.negativeInfinity if the camera is not facing the player. Returns null
    /// if the game is currently paused.</returns>
    protected override IEnumerator HandleRotation(CameraController activeCamera)
    {
        while (true)
        {
            while (GameManager.gm.IsGameRunning)
            {
                // Create a plane object for the plane the player is standing on
                Plane groundPlane = new Plane(Vector3.up, ThisPawn.PawnData.PawnTransform.position);

                // Create a ray from camera through the mouse position in the direction the camera is facing
                if (activeCamera != null)
                {
                    // Uses the mouse position to rotate the pawn
                    Ray mouseRay = activeCamera.ThisCamera.ScreenPointToRay(Input.mousePosition);

                    // Raycast to find where the mouse ray intersects with our ground plane
                    float intersectDistance;
                    if (groundPlane.Raycast(mouseRay, out intersectDistance))
                    {
                        // Gets the point in world space where the mouse is aiming
                        Vector3 intersectPoint = mouseRay.GetPoint(intersectDistance);

                        // Get the rotation needed for the player to look at that point
                        Quaternion targetRotation =
                            Quaternion.LookRotation(intersectPoint - ThisPawn.PawnData.PawnTransform.position, ThisPawn.PawnData.PawnTransform.up);
                        ThisPawn.PawnData.PawnTransform.rotation = Quaternion.RotateTowards(ThisPawn.PawnData.PawnTransform.rotation, targetRotation,
                            ThisPawn.PawnData.TurnSpeed * Time.deltaTime);
                        yield return intersectPoint;
                    }
                    else
                    {
#if UNITY_EDITOR
                        Debug.LogError("Camera is not looking at plane");
                        yield return Vector3.negativeInfinity;
#endif
                    }

                }

                yield return null;
            }

            yield return null;
        }
    }
}
