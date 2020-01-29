using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class AgentController : MonoBehaviour
{
    /* Public Properties */
    [Tooltip("The movement speed of this pawn")] public float moveSpeed = 3.5f;
    [Tooltip("The turning speed of this pawn")] public float turnSpeed = 60f;

    [Space, Tooltip("The camera that will be following this pawn's movements")]public Camera pawnCamera;

    /* Protected Properties */
    [Header("Component References")]
    [SerializeField, Tooltip("This object's Transform component")] protected Transform tf;
    [SerializeField, Tooltip("This object's Animator component")] protected Animator anim;
    [SerializeField, Tooltip("The agent's collider")] protected Collider collider;

    /* Private Properties*/ 

    // Start is called before the first frame update
    public virtual void Start()
    {
        /* Component reference assignments */
        tf = this.GetComponent<Transform>();
        anim = this.GetComponent<Animator>();
        collider = this.GetComponent<Collider>();
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
                anim.SetBool("isSprinting", false);
                anim.SetBool("isCrouching", false);
                break;
            case LocomotionState.Crouching:
                anim.SetBool("isSprinting", false);
                anim.SetBool("isCrouching", true);
                break;
            case LocomotionState.Sprinting:
                anim.SetBool("isSprinting", true);
                anim.SetBool("isCrouching", false);
                break;
        }
    }
}
