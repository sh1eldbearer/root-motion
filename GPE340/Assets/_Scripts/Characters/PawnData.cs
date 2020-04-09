﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PawnData : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("")]
    [SerializeField] private Weapon _equippedWeapon;

        [Header("Movement Settings")]
    [Tooltip("The movement speed of this pawn."),
        SerializeField] private float _moveSpeed = 7f;
    [Tooltip("The turning speed of this pawn."),
        SerializeField] private float _turnSpeed = 720f;

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
    [Tooltip("The Transform component of this pawn's model's head."),
        SerializeField] private Transform _headTransform;
    [Space, SerializeField] private List<SkinnedMeshRenderer> _modelMeshes = new List<SkinnedMeshRenderer>();
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The movement speed of this agent.
    /// </summary>
    public float MoveSpeed
    {
        get { return _moveSpeed; }
    }

    /// <summary>
    /// The turn speed of this agent.
    /// </summary>
    public float TurnSpeed
    {
        get { return _turnSpeed; }
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
        get { return new Vector3(_pawnCollider.center.x ,_standColliderCenterY, 
            _pawnCollider.center.z); }
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
        get { return new Vector3(_pawnCollider.center.x, _crouchColliderCenterY, 
            _pawnCollider.center.z); }
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

    // TODO: Move once this system is finalized
    public Weapon EquippedWeapon
    {
        get { return _equippedWeapon; }
    }

    /// <summary>
    /// The Transform component of this pawn's model's head.
    /// </summary>
    public Transform HeadTransform
    {
        get { return _headTransform; }
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
            _pawnTransform = this.gameObject.GetComponentInChildren<Transform>();
        }
        if (_pawnAnimator == null)
        {
            _pawnAnimator = this.gameObject.GetComponentInChildren<Animator>();
        }
        if (_pawnCollider == null)
        {
            _pawnCollider = this.gameObject.GetComponentInChildren<CapsuleCollider>();
        }

        // Stores the SkinnedMeshRenderers of each part of the character so we can modify their materials
        _modelMeshes = this.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
    }
}