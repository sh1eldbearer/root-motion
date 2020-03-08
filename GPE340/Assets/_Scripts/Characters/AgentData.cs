using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentData : MonoBehaviour
{

    #region Private Properties
#pragma warning disable CS0649
    [Header("Movement Settings")]
    [Tooltip("The movement speed of this agent."),
     SerializeField]
    private float _moveSpeed = 7f;
    [Tooltip("The turning speed of this agent."),
     SerializeField]
    private float _turnSpeed = 720f;

    [Header("Collider Settings")]
    [Tooltip("The height the collider should be when the agent is standing."),
        SerializeField] private float _standColliderHeight = 2f;
    [Tooltip("The position the center of the collider should be at when the agent is standing."),
        SerializeField] private Vector3 _standColliderCenter = new Vector3(0f, 1f, 0f);
    [Tooltip("The height the collider should be when the agent is crouching."),
        SerializeField] private float _crouchColliderHeight = 1f;
    [Tooltip("The position the center of the collider should be at when the agent is crouching."),
        SerializeField] private Vector3 _crouchColliderCenter = new Vector3(0f, 0.5f, 0f);
    [Tooltip("The speed at which the collider's height and center position should adjust."),
        SerializeField] private float _colliderAdjustSpeed = 5f;

    [Header("Game Components")]
    [Tooltip("This agent's controller."),
        SerializeField] private AgentController _controller;
    [Tooltip("This agent's Transform component."),
        SerializeField] private Transform _agentTransform;
    [Tooltip("This agent's Animator component."),
        SerializeField] private Animator _agentAnimator;
    [Tooltip("The Capsule Collider attached to this agent."),
        SerializeField] private CapsuleCollider _agentCollider;
    [Tooltip("The camera that will be following this agent's movements."),
        SerializeField] private CameraController _agentCamera;
#pragma warning restore CS0649
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
    /// The height the collider should be when the agent is standing.
    /// </summary>
    public float StandColliderHeight
    {
        get { return _standColliderHeight; }
    }


    /// <summary>
    /// The position the center of the collider should be at when the agent is standing.
    /// </summary>
    public Vector3 StandColliderCenter
    {
        get { return _standColliderCenter; }
    }

    /// <summary>
    /// The height the collider should be when the agent is crouching.
    /// </summary>
    public float CrouchColliderHeight
    {
        get { return _crouchColliderHeight; }
    }

    /// <summary>
    /// The position the center of the collider should be at when the agent is crouching.
    /// </summary>
    public Vector3 CrouchColliderCenter
    {
        get { return _crouchColliderCenter; }
    }

    /// <summary>
    /// The speed at which the collider's height and center position should adjust.
    /// </summary>
    public float ColliderAdjustSpeed
    {
        get { return _colliderAdjustSpeed; }
    }

    /// <summary>
    /// This agent's controller.
    /// </summary>
    public AgentController Controller
    {
        get { return _controller; }
    }
    /// <summary>
    /// This agent's Transform component.
    /// </summary>
    public Transform AgentTransform
    {
        get { return _agentTransform; }
    }
    /// <summary>
    /// This agent's Animator component.
    /// </summary>
    public Animator AgentAnimator
    {
        get { return _agentAnimator; }
    }

    /// <summary>
    /// The Capsule Collider attached to this agent.
    /// </summary>
    public CapsuleCollider AgentCollider
    {
        get { return _agentCollider; }
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
    private void Start()
    {
        // Component reference assignments
        if (_controller == null)
        {
            _controller = this.gameObject.GetComponent<AgentController>();
        }
        if (_agentTransform == null)
        {
            _agentTransform = this.transform;
        }
        if (_agentAnimator == null)
        {
            _agentAnimator = this.gameObject.GetComponent<Animator>();
        }
        if (_agentCollider == null)
        {
            _agentCollider = this.gameObject.GetComponent<CapsuleCollider>();
        }
    }

    /// <summary>
    /// Assigns a camera to follow this agent.
    /// </summary>
    /// <param name="camera">The camera that should follow this agent.</param>
    public void AssignCameraController(CameraController camera)
    {
        _agentCamera = camera;
    }
}
