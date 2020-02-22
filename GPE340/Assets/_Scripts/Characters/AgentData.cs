using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentData : MonoBehaviour
{
    #region Private Properties
    [Space, Tooltip("This agent's controller."),
        SerializeField] private AgentController _controller;
    [Tooltip("This agent's Transform component."),
        SerializeField] private Transform _agentTransform;
    [Tooltip("This agent's Animator component."),
        SerializeField] private Animator _agentAnimator;
    [Space, Tooltip("The camera that will be following this agent's movements."),
        SerializeField] private CameraController _agentCamera;
    #endregion

    #region Public Properties
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
    void Awake()
    {
        // Component reference assignments
        _controller = this.gameObject.GetComponent<AgentController>();
        _agentTransform = this.gameObject.GetComponent<Transform>();
        _agentAnimator = this.gameObject.GetComponent<Animator>();
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
