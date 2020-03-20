using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]

public class PawnData : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("Movement Settings")]
    [Tooltip("The movement speed of this pawn."),
     SerializeField]
    private float _moveSpeed = 7f;
    [Tooltip("The turning speed of this pawn."),
     SerializeField]
    private float _turnSpeed = 720f;

    [Header("Collider Settings")]
    [Tooltip("The height the collider should be when the pawn is standing."),
        SerializeField] private float _standColliderHeight = 2f;
    [Tooltip("The position the center of the collider should be at when the pawn is standing."),
        SerializeField] private float _standColliderCenterY = 1f;
    [Tooltip("The height the collider should be when the pawn is crouching."),
        SerializeField] private float _crouchColliderHeight = 1f;
    [Tooltip("The position the center of the collider should be at when the pawn is crouching."),
        SerializeField] private float _crouchColliderCenterY = 0.5f;
    [Tooltip("The speed at which the pawn's collider height and center position should adjust."),
        SerializeField] private float _colliderAdjustSpeed = 20f;

    [Header("Game Components")]
    [Tooltip("This pawn's controller."),
        SerializeField] private AgentController _controller;
    [Tooltip("This pawn's Transform component."),
        SerializeField] private Transform _pawnTransform;
    [Tooltip("This pawn's Animator component."),
        SerializeField] private Animator _pawnAnimator;
    [Tooltip("The Capsule Collider attached to this pawn."),
        SerializeField] private CapsuleCollider _pawnCollider;
    [Tooltip("The camera that will be following this pawn's movements."),
        SerializeField] private CameraController _pawnCamera;
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
        get { return new Vector3(0f,_standColliderCenterY, 0f); }
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
        get { return new Vector3(0f, _crouchColliderCenterY, 0f); }
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
    public Transform PawnTransform
    {
        get { return _pawnTransform; }
    }
    /// <summary>
    /// This agent's Animator component.
    /// </summary>
    public Animator PawnAnimator
    {
        get { return _pawnAnimator; }
    }

    /// <summary>
    /// The Capsule Collider attached to this agent.
    /// </summary>
    public CapsuleCollider PawnCollider
    {
        get { return _pawnCollider; }
    }

    /// <summary>
    /// The camera that will be following this agent's movements.
    /// </summary>
    public CameraController PawnCamera
    {
        get { return _pawnCamera; }
    }

    #endregion

    // Awake is called before Start
    private void Awake()
    {
        // Component reference assignments
        if (_controller == null)
        {
            _controller = this.gameObject.GetComponentInParent<AgentController>();
        }
        if (_pawnTransform == null)
        {
            _pawnTransform = this.transform;
        }
        if (_pawnAnimator == null)
        {
            _pawnAnimator = this.gameObject.GetComponentInChildren<Animator>();
        }
        if (_pawnCollider == null)
        {
            _pawnCollider = this.gameObject.GetComponentInChildren<CapsuleCollider>();
        }
    }

    /// <summary>
    /// Assigns a camera to follow this agent.
    /// </summary>
    /// <param name="camera">The camera that should follow this agent.</param>
    public void AssignCameraController(CameraController camera)
    {
        _pawnCamera = camera;
    }
}
