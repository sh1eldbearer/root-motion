using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentData : MonoBehaviour
{

    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The movement speed of this agent."),
     SerializeField]
    private float _moveSpeed = 7f;
    [Tooltip("The turning speed of this agent."),
     SerializeField]
    private float _turnSpeed = 720f;

    [Space, Tooltip("This agent's controller."),
        SerializeField] private AgentController _controller;
    [Tooltip("This agent's Transform component."),
        SerializeField] private Transform _agentTransform;
    [Tooltip("This agent's Animator component."),
        SerializeField] private Animator _agentAnimator;
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
