﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class AgentController : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("Component References")]
    [Tooltip("This agent's Pawn component."),
     SerializeField] private Pawn _thisPawn;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// This agent's Pawn component.
    /// </summary>
    public Pawn ThisPawn
    {
        get { return _thisPawn; }
    }
    #endregion

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // TODO: Determine base functionality once AI controllers are set up

        // Component reference assignments
        _thisPawn = _thisPawn ?? this.gameObject.GetComponentInChildren<Pawn>();
    }

    /// <summary>
    /// Moves the agent relative to local space.
    /// </summary>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    protected virtual IEnumerator Move()
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return Vector3.negativeInfinity;
    }

    /// <summary>
    /// Rotates the agent relative to local space.
    /// </summary>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    protected virtual IEnumerator HandleRotation()
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return Vector3.negativeInfinity;
    }

    /// <summary>
    /// Rotates the agent relative to world space.
    /// </summary>
    /// <param name="activeCamera"></param>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    protected virtual IEnumerator HandleRotation(CameraController activeCamera)
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return Vector3.negativeInfinity;
    }

    /// <summary>
    /// Sets the appropriate animator booleans in order to use the designated movement type for
    /// the agent.
    /// </summary>
    /// <param name="locoState">The current movement state of the agent.</param>
    protected void SetLocomotionState(LocomotionState locoState)
    {
        switch (locoState)
        {
            case LocomotionState.Walking:
                _thisPawn.PawnData.PawnAnimator.SetBool("isSprinting", false);
                _thisPawn.PawnData.PawnAnimator.SetBool("isCrouching", false);
                break;
            case LocomotionState.Crouching:
                _thisPawn.PawnData.PawnAnimator.SetBool("isSprinting", false);
                _thisPawn.PawnData.PawnAnimator.SetBool("isCrouching", true);
                break;
            case LocomotionState.Sprinting:
                _thisPawn.PawnData.PawnAnimator.SetBool("isSprinting", true);
                _thisPawn.PawnData.PawnAnimator.SetBool("isCrouching", false);
                break;
        }
    }
}
