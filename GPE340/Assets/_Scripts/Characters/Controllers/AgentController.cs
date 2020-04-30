using System.Collections;
using UnityEngine;
using Utility.Enums;

public abstract class AgentController : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
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
        // TODO: Determine base functionality once AI controllers are set up

        // Component reference assignments
        if (_thisPawn == null)
        {
            _thisPawn = this.gameObject.GetComponentInChildren<Pawn>();
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {

    }

    /// <summary>
    /// Start all coroutines used by this class.
    /// </summary>
    public virtual void StartAllCoroutines()
    {
        StartMoveCoroutine();
        StartRotationCoroutine();
        StartFireInputCoroutine();
    }

    /// <summary>
    /// Starts the Move coroutine for this agent.
    /// </summary>
    protected void StartMoveCoroutine()
    {
        StartCoroutine(Move());
    }

    /// <summary>
    /// Stops the Move coroutine for this agent.
    /// </summary>
    protected void StopMoveCoroutine()
    {
        StopCoroutine(Move());

    }

    /// <summary>
    /// Starts the Rotate coroutine for this agent.
    /// </summary>
    protected void StartRotationCoroutine()
    {
        StartCoroutine(Rotate());
    }

    /// <summary>
    /// Stops the Rotate coroutine for this agent.
    /// </summary>
    protected void StopRotationCoroutine()
    {
        StopCoroutine(Rotate());
    }

    /// <summary>
    /// Starts the WaitForFireWeaponInput coroutine for this agent.
    /// </summary>
    protected void StartFireInputCoroutine()
    {
        StartCoroutine(WaitForFireWeaponInput());
    }

    /// <summary>
    /// Stops the WaitForFireWeaponInput coroutine for this agent.
    /// </summary>
    protected void StopFireInputCoroutine()
    {
        StopCoroutine(WaitForFireWeaponInput());
    }

    /// <summary>
    /// Moves the agent relative to local space.
    /// </summary>
    /// <returns>Coroutine.</returns>
    protected virtual IEnumerator Move()
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return null;
    }

    /// <summary>
    /// Rotates the agent relative to local space.
    /// </summary>
    /// <returns>Coroutine.</returns>
    protected virtual IEnumerator Rotate()
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return null;
    }

    /// <summary>
    /// Rotates the agent relative to world space.
    /// </summary>
    /// <param name="activeCamera"></param>
    /// <returns>Coroutine.</returns>
    protected virtual IEnumerator Rotate(CameraController activeCamera)
    {
        // TODO: Determine base functionality once AI controllers are set up
        yield return null;
    }

    /// <summary>
    /// Fires the currently equipped weapon.
    /// </summary>
    /// <returns>Coroutine.</returns>
    protected virtual IEnumerator WaitForFireWeaponInput()
    {
        yield return null;
    }

    /// <summary>
    /// Sets the appropriate animator booleans in order to use the designated movement type for
    /// the agent.
    /// </summary>
    /// <param name="locoState">The current movement state of the agent.</param>
    protected void SetLocomotionState(LocomotionState locoState)
    {
        // Set the new locomotion state
        switch (locoState)
        {
            case LocomotionState.Walking:
                _thisPawn.PawnData.PawnAnimator.SetBool("isSprinting", false);
                _thisPawn.PawnData.PawnAnimator.SetBool("isCrouching", false);
                break;
            case LocomotionState.Crouching:
                _thisPawn.PawnData.PawnAnimator.SetBool("isSprinting", false);
                _thisPawn.PawnData.PawnAnimator.SetBool("isCrouching", true);
                break;
            case LocomotionState.Sprinting:
                _thisPawn.PawnData.PawnAnimator.SetBool("isSprinting", true);
                _thisPawn.PawnData.PawnAnimator.SetBool("isCrouching", false);
                break;
        }
    }
}
