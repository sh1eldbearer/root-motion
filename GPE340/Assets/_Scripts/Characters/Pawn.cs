using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PawnData))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

public class Pawn : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("Component References")]
    [Tooltip("The PawnData component for this Pawn."), 
        SerializeField] private PawnData _pawnData;

#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The PawnData component for this Pawn.
    /// </summary>
    public PawnData PawnData
    {
        get { return _pawnData; }
    }
    #endregion

    // Awake is called before Start
    private void Awake()
	{
		// Component reference assignments
        if (PawnData == null)
        {
            _pawnData = this.gameObject.GetComponent<PawnData>();
        }
	}

    /// <summary>
    /// Movement behaviors for an AI-controlled Pawn. Not yet implemented.
    /// </summary>
    public void Move()
    {
#if UNITY_EDITOR
        Debug.Log("Wrong Pawn.Move function, dude.");
#endif
    }

    /// <summary>
    /// Movement behaviors for a player-controlled Pawn.
    /// </summary>
    /// <param name="horizontalInput">The input from the horizontal input axis the player is using.</param>
    /// <param name="verticalInput">The input from the vertical input axis the player is using.</param>
    public void Move(float horizontalInput, float verticalInput)
    {
        // Get the world vector that we want to move
        Vector3 worldMoveVector = new Vector3(horizontalInput, 0f, verticalInput);
        // Normalize it to allow controllers and keyboards to function the same
        Vector3.ClampMagnitude(worldMoveVector, 1f);

        // Find local version of the worldMoveVector (relative to the object's transform)
        Vector3 localMoveVector = PawnData.PawnTransform.InverseTransformDirection(worldMoveVector);

        // Pass values from the input controller into the animator to generate movement
        PawnData.PawnAnimator.SetFloat("Horizontal", localMoveVector.x * PawnData.MoveSpeed);
        PawnData.PawnAnimator.SetFloat("Vertical", localMoveVector.z * PawnData.MoveSpeed);

        PawnData.PawnCamera.UpdateCameraPosition();
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
                _pawnData.PawnAnimator.SetBool("isSprinting", false);
                _pawnData.PawnAnimator.SetBool("isCrouching", false);
                break;
            case LocomotionState.Crouching:
                _pawnData.PawnAnimator.SetBool("isSprinting", false);
                _pawnData.PawnAnimator.SetBool("isCrouching", true);
                break;
            case LocomotionState.Sprinting:
                _pawnData.PawnAnimator.SetBool("isSprinting", true);
                _pawnData.PawnAnimator.SetBool("isCrouching", false);
                break;
        }
    }
}
