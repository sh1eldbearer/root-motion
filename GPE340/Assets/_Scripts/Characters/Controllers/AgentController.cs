using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public abstract class AgentController : MonoBehaviour
{
    /// <summary>
    /// Defines which set of animations the agent should use to move.
    /// </summary>
    protected enum LocomotionState
    {
        Walking = 0,
        Crouching = 1,
        Sprinting = 2
    }

    #region Private Properties
    [Tooltip("The movement speed of this agent."),
        SerializeField] private float _moveSpeed = 3.5f;
    [Tooltip("The turning speed of this agent."),
        SerializeField] private float _turnSpeed = 60f;

    [Header("Component References")]
    [Tooltip("This agent's Transform component."),
        SerializeField] private Transform _agentTransform;
    [Tooltip("This agent's Animator component."),
        SerializeField] private Animator _agentAnimator;

    [Space, Tooltip("The camera that will be following this agent's movements."),
        SerializeField] private CameraController _agentCamera;
    #endregion

    #region Public Properties
    /// <summary>
    /// The movement speed of this agent.
    /// </summary>
    public float MoveSpeed
    {
        get { return _moveSpeed; }
        protected set { _moveSpeed = value; }
    }

    /// <summary>
    /// The turn speed of this agent.
    /// </summary>
    public float TurnSpeed
    {
        get { return _turnSpeed; }
        protected set { _turnSpeed = value; }
    }

    /// <summary>
    /// The Animator component for this agent.
    /// </summary>
    public Animator AgentAnimator
    {
        get { return _agentAnimator; }
        protected set { _agentAnimator = value; }
    }

    /// <summary>
    /// The Transform component for this agent.
    /// </summary>
    public Transform AgentTransform
    {
        get { return _agentTransform; }
        protected set { _agentTransform = value; }
    }
    /// <summary>
    /// The camera that will be following this agent's movements.
    /// </summary>
    public CameraController AgentCamera
    {
        get { return _agentCamera; }
    }
    #endregion

    // Start is called before the first frame update
    public virtual void Start()
    {
        /* Component reference assignments */
        _agentTransform = this.GetComponent<Transform>();
        _agentAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
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
                _agentAnimator.SetBool("isSprinting", false);
                _agentAnimator.SetBool("isCrouching", false);
                break;
            case LocomotionState.Crouching:
                _agentAnimator.SetBool("isSprinting", false);
                _agentAnimator.SetBool("isCrouching", true);
                break;
            case LocomotionState.Sprinting:
                _agentAnimator.SetBool("isSprinting", true);
                _agentAnimator.SetBool("isCrouching", false);
                break;
        }
    }

    /// <summary>
    /// Assigns a camera to follow this agent.
    /// </summary>
    /// <param name="camera">The camera that should follow this agent.</param>
    /// <returns>The camera that is assigned to follow this agent (for debugging purposes.)</returns>
    public CameraController AssignCamera(Camera camera)
    {
        _agentCamera = camera.GetComponent<CameraController>();
        return _agentCamera;
    }
}
