﻿using System.Collections;
using UnityEditor.Animations;
using UnityEngine;
using Utility.Enums;

public class PlayerController : AgentController
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        // Register coroutines with the pause manager
        PauseManager.pauseMgr.AddOnUnpauseListeners(StartMoveCoroutine, StartRotationCoroutine);
        PauseManager.pauseMgr.AddOnPauseListeners(StopMoveCoroutine, StopRotationCoroutine);
    }

    protected override void OnDisable()
    {
        // Unregister coroutines with the pause manager
        PauseManager.pauseMgr.RemoveOnUnpauseListeners(StartMoveCoroutine, StartRotationCoroutine);
        PauseManager.pauseMgr.RemoveOnPauseListeners(StopMoveCoroutine, StopRotationCoroutine);
    }

    private void StartMoveCoroutine()
    {
        StartCoroutine(Move());
    }

    private void StopMoveCoroutine()
    {
        StopCoroutine(Move());

    }

    private void StartRotationCoroutine()
    {
        StartCoroutine(HandleRotation());
    }

    private void StopRotationCoroutine()
    {
        StopCoroutine(HandleRotation());
    }

    /// <summary>
    /// Moves the agent, relative to local space.
    /// </summary>
    /// <returns>A co-routine enumerator.</returns>
    protected override IEnumerator Move()
    {
        while (true)
        {
            // Set sprinting or walking states if any of the inputs are pressed
            if (Input.GetAxis("Sprint") > 0f && Input.GetAxis("Crouch") > 0f)
            {
                SetLocomotionState(LocomotionState.Walking);
            }
            else if (Input.GetAxis("Sprint") > 0f)
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

            // Move the pawn
            ThisPawn.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                
            // If there is a camera following this pawn, updates the camera position
            if (GameManager.gm.GameCameraController.FollowTf == ThisPawn.PawnData.PawnTransform)
            {
                GameManager.gm.GameCameraController.UpdateCameraPosition();
            }

            yield return null;
        }
    }

    /// <summary>
    /// Rotates the agent to face where the mouse is pointing, relative to local space.
    /// </summary>
    /// <param name="activeCamera">The currently active camera in the game scene.</param>
    protected override IEnumerator HandleRotation()
    {
        while (true)
        {
            // Create a plane object for the plane the player is standing on
            Plane groundPlane = new Plane(Vector3.up, ThisPawn.PawnData.PawnTransform.position);

            // Uses the mouse position to rotate the pawn
            Ray mouseRay = GameManager.gm.GameCamera.ScreenPointToRay(Input.mousePosition);

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
            }

            yield return null;
        }
    }
}