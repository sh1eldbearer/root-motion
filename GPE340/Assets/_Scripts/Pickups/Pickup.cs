using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public abstract class Pickup : MonoBehaviour, IPickupable
{
    #region Private Properties
#pragma warning disable CS0649
    private Collider _pickupCollider;
    private Rigidbody _pickupRb;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The Collider component for this weapon pickup
    /// </summary>
    public Collider PickupCollider
    {
        get { return _pickupCollider; }
        protected set { _pickupCollider = value; }
    }

    /// <summary>
    /// The Rigidbody component for this weapon pickup
    /// </summary>
    public Rigidbody PickupRb
    {
        get { return _pickupRb; }
        protected set { _pickupRb = value; }
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

    public virtual void OnTriggerEnter(Collider collider)
    {

    }

    public virtual void OnPickup(Collider collider)
    {

    }
}
