using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))] 
[RequireComponent(typeof(Animator), typeof(AgentData))]
public abstract class AgentController : MonoBehaviour
{
    /// <summary>
    /// Designates which set of animations the agent should use to move.
    /// </summary>
    protected enum LocomotionState
    {
        Walking = 0,
        Crouching = 1,
        Sprinting = 2
    }

    #region Private Properties
#pragma warning disable CS0649
    [Header("Component References")]
    [Tooltip("This agent's data component."),
        SerializeField] private AgentData _agentData;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// This agent's data component.
    /// </summary>
    public AgentData AgentData
    {
        get { return _agentData; }
    }
    #endregion

    // Start is called before the first frame update
    public virtual void Start()
    {
        // TODO: Determine base functionality once AI controllers are set up
    }

    /// <summary>
    /// Moves the agent relative to local space.
    /// </summary>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    public virtual IEnumerator Move()
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return Vector3.negativeInfinity;
    }

    /// <summary>
    /// Rotates the agent relative to local space.
    /// </summary>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    public virtual IEnumerator HandleRotation()
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return Vector3.negativeInfinity;
    }

    /// <summary>
    /// Rotates the agent relative to world space.
    /// </summary>
    /// <param name="activeCamera"></param>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    public virtual IEnumerator HandleRotation(CameraController activeCamera)
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return Vector3.negativeInfinity;
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
                _agentData.AgentAnimator.SetBool("isSprinting", false);
                _agentData.AgentAnimator.SetBool("isCrouching", false);
                break;
            case LocomotionState.Crouching:
                _agentData.AgentAnimator.SetBool("isSprinting", false);
                _agentData.AgentAnimator.SetBool("isCrouching", true);
                break;
            case LocomotionState.Sprinting:
                _agentData.AgentAnimator.SetBool("isSprinting", true);
                _agentData.AgentAnimator.SetBool("isCrouching", false);
                break;
        }
    }
}
