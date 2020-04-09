using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class AgentController : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [SerializeField] private UnityEvent _onCrouch = new UnityEvent();
    [SerializeField] private UnityEvent _onStand = new UnityEvent();

    [Header("Component References")]
    [Tooltip("This agent's Pawn component."),
        SerializeField] private Pawn _thisPawn;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// This agent's Pawn component.
    /// </summary>
    public Pawn ThisPawn
    {
        get { return _thisPawn; }
    }
    #endregion

    // Awake is called before Start
    protected virtual void Awake()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // TODO: Determine base functionality once AI controllers are set up

        // Component reference assignments
        if (_thisPawn == null)
        {
            _thisPawn = this.gameObject.GetComponentInChildren<Pawn>();
        }
    }

    /// <summary>
    /// Moves the agent relative to local space.
    /// </summary>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    protected virtual IEnumerator Move()
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return Vector3.negativeInfinity;
    }

    /// <summary>
    /// Rotates the agent relative to local space.
    /// </summary>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    protected virtual IEnumerator HandleRotation()
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return Vector3.negativeInfinity;
    }

    /// <summary>
    /// Rotates the agent relative to world space.
    /// </summary>
    /// <param name="activeCamera"></param>
    /// <returns>Vector3.negativeInfinity as a placeholder value.</returns>
    protected virtual IEnumerator HandleRotation(CameraController activeCamera)
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return Vector3.negativeInfinity;
    }

    /// <summary>
    /// Adds a listener to the OnCrouch event.
    /// </summary>
    /// <param name="call">The name of the function to call when OnCrouch is invoked.</param>
    public void AddOnCrouchListener(UnityAction call)
    {
        _onCrouch.AddListener(call);
    }

    /// <summary>
    /// Removes a listener from the OnCrouch event.
    /// </summary>
    /// <param name="call">The name of the function to remove from the OnCrouch invoke array.</param>
    public void RemoveOnCrouchListener(UnityAction call)
    {
        _onCrouch.RemoveListener(call);
    }

    /// <summary>
    /// Adds a listener to the OnStand event.
    /// </summary>
    /// <param name="call">The name of the function to call when OnStand is invoked.</param>
    public void AddOnStandListener(UnityAction call)
    {
        _onStand.AddListener(call);
    }

    /// <summary>
    /// Removes a listener from the OnStand event.
    /// </summary>
    /// <param name="call">The name of the function to remove from the OnStand invoke array.</param>
    public void RemoveOnStandListener(UnityAction call)
    {
        _onStand.RemoveListener(call);
    }

    /// <summary>
    /// Sets the appropriate animator booleans in order to use the designated movement type for
    /// the agent.
    /// </summary>
    /// <param name="locoState">The current movement state of the agent.</param>
    protected void SetLocomotionState(Enums.LocomotionState locoState)
    {
        switch (locoState)
        {
            case Enums.LocomotionState.Walking:
                _thisPawn.PawnData.PawnAnimator.SetBool("isSprinting", false);
                _thisPawn.PawnData.PawnAnimator.SetBool("isCrouching", false);
                _onStand.Invoke();
                break;
            case Enums.LocomotionState.Crouching:
                _thisPawn.PawnData.PawnAnimator.SetBool("isSprinting", false);
                _thisPawn.PawnData.PawnAnimator.SetBool("isCrouching", true);
                _onCrouch.Invoke();
                break;
            case Enums.LocomotionState.Sprinting:
                _thisPawn.PawnData.PawnAnimator.SetBool("isSprinting", true);
                _thisPawn.PawnData.PawnAnimator.SetBool("isCrouching", false);
                _onStand.Invoke();
                break;
        }
    }
}
