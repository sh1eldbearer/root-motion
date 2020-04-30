using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public abstract class Pickup : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("Game Components")]
    [Tooltip("The Collider component for this pickup object."),
    SerializeField] private Collider _pickupCollider;
    [Tooltip("The Rigidbody component for this pickup object."),
     SerializeField] private Rigidbody _pickupRb;
    [Tooltip("The Transform component for this pickup object."),
     SerializeField] private Transform _pickupTransform;
    
    private Vector3 _initialPosition; // The initial Transform.position of this pickup
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The Collider component for this pickup object.
    /// </summary>
    public Collider PickupCollider
    {
        get { return _pickupCollider; }
    }

    /// <summary>
    /// The Rigidbody component for this pickup object.
    /// </summary>
    public Rigidbody PickupRb
    {
        get { return _pickupRb; }
    }

    #endregion

    // Awake is called before Start
    public virtual void Awake()
    {
        // Component reference assignments
        if (_pickupCollider == null)
        {
            _pickupCollider = this.gameObject.GetComponent<Collider>();
        }
        if (_pickupRb == null)
        {
            _pickupRb = this.gameObject.GetComponent<Rigidbody>();
        }
        if (_pickupTransform == null)
        {
            _pickupTransform = this.gameObject.transform;
        }

        // Stores the initial position of the pickup
        _initialPosition = _pickupTransform.position;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Ensure the pickup's collider is a trigger
        _pickupCollider.isTrigger = true;

        // Ensure the pickup's rigidbody isn't using gravity
        _pickupRb.useGravity = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    private void OnEnable()
    {
        // Register listeners with pause manager
        PauseManager.pauseMgr.AddOnPauseListener(StopSpinCoroutine, StopBounceCoroutine);
        PauseManager.pauseMgr.AddOnUnpauseListener(StartSpinCoroutine, StartBounceCoroutine);

        StartAllCoroutines();
    }

    private void OnDisable()
    {
        // Unregister listeners with pause manager
        PauseManager.pauseMgr.RemoveOnPauseListener(StopSpinCoroutine, StopBounceCoroutine);
        PauseManager.pauseMgr.RemoveOnUnpauseListener(StartSpinCoroutine, StartBounceCoroutine);

        StopAllCoroutines();
    }

    /// <summary>
    /// Start all coroutines used by this class.
    /// </summary>
    private void StartAllCoroutines()
    {
        StartSpinCoroutine();
        StartBounceCoroutine();
    }

    /// <summary>
    /// Starts the Spin coroutine for this agent.
    /// </summary>
    private void StartSpinCoroutine()
    {
        StartCoroutine(Spin(Vector3.up));
    }

    /// <summary>
    /// Stops the Spin coroutine for this agent.
    /// </summary>
    private void StopSpinCoroutine()
    {
        StopCoroutine(Spin(Vector3.up));
    }

    /// <summary>
    /// Starts the Bounce coroutine for this agent.
    /// </summary>
    private void StartBounceCoroutine()
    {
        StartCoroutine(Bounce());
    }

    /// <summary>
    /// Stops the Bounce coroutine for this agent.
    /// </summary>
    private void StopBounceCoroutine()
    {
        StopCoroutine(Bounce());
    }

    /// <summary>
    /// Rotates the pickup object around the specified axis.
    /// </summary>
    /// <returns>Coroutine.</returns>
    private IEnumerator Spin(Vector3 axis)
    {
        while (true)
        {
            _pickupTransform.Rotate(axis, Time.deltaTime * GameManager.gm.PickupSpinSpeed);
            yield return null;
        }
    }

    /// <summary>
    /// Bounces the pickup object up and down.
    /// </summary>
    /// <returns>Coroutine.</returns>
    private IEnumerator Bounce()
    {
        while (true)
        {
            _pickupTransform.position = _initialPosition + new Vector3(0f,
                                            Mathf.Sin(Time.time * GameManager.gm.PickupBounceSpeed) *
                                            GameManager.gm.PickupBounceHeight, 0f);
            yield return null;
        }
    }
}
