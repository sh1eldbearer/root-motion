using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class AgentController : MonoBehaviour
{
    /* Public Properties */

    /// <summary>
    /// The movement speed of this pawn.
    /// </summary>
    public float moveSpeed
    {
        get { return _moveSpeed; }
    }

    /// <summary>
    /// The turn speed of this pawn.
    /// </summary>
    public float turnSpeed
    {
        get { return _turnSpeed; }
    }

    /* Protected
     Properties */
    [Tooltip("The movement speed of this pawn")] protected float _moveSpeed = 3.5f;
    [Tooltip("The turning speed of this pawn")] protected float _turnSpeed = 60f;

    [Header("Component References")]
    [Tooltip("This object's Transform component")] protected Transform _agentTf;
    [Tooltip("This object's Animator component")] protected Animator _agentAnim;

    /* Private Properties*/
    [Space, Tooltip("The camera that will be following this pawn's movements")] public Camera pawnCamera;

    // Start is called before the first frame update
    public virtual void Start()
    {
        /* Component reference assignments */
        _agentTf = this.GetComponent<Transform>();
        _agentAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    /// <summary>
    /// Moves the agent.
    /// </summary>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    public virtual Vector3 Move()
    {
        // TODO: Determine base functionality once AI controllers are set up
        return Vector3.negativeInfinity;
    }

    /// <summary>
    /// Rotates the agent relative to local space.
    /// </summary>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    public virtual Vector3 HandleRotation()
    {
        // TODO: Determine base functionality once AI controllers are set up
        return Vector3.negativeInfinity;
    }
    /// <summary>
    /// Rotates the agent relative to world space.
    /// </summary>
    /// <param name="activeCamera"></param>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    public virtual Vector3 HandleRotation(Camera activeCamera)
    {
        // TODO: Determine base functionality once AI controllers are set up
        return Vector3.negativeInfinity;
    }

    /// <summary>
    /// Defines which set of animations the agent should use to move.
    /// </summary>
    protected enum LocomotionState
    {
        Walking = 0,
        Crouching = 1,
        Sprinting = 2
    }

    /// <summary>
    /// Sets the appropriate animator booleans in order to use the designated movement type for
    /// the agent.
    /// </summary>
    /// <param name="locoState">The current movement state of the agent.</param>
    protected void ChangeLocomotionState(LocomotionState locoState)
    {
        switch (locoState)
        {
            case LocomotionState.Walking:
                _agentAnim.SetBool("isSprinting", false);
                _agentAnim.SetBool("isCrouching", false);
                break;
            case LocomotionState.Crouching:
                _agentAnim.SetBool("isSprinting", false);
                _agentAnim.SetBool("isCrouching", true);
                break;
            case LocomotionState.Sprinting:
                _agentAnim.SetBool("isSprinting", true);
                _agentAnim.SetBool("isCrouching", false);
                break;
        }
    }
}
