using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public abstract class Pickup : MonoBehaviour, IPickupHandler
{
    #region Private Properties
#pragma warning disable CS0649
    private Collider _pickupCollider;
    private Rigidbody _pickupRb;
#pragma warning restore CS0649
    #endregion

    #region Public Properties

    #endregion
	
	// Awake is called before Start
	public virtual void Awake()
	{
        // Component reference assignments
        _pickupCollider = _pickupCollider ?? this.gameObject.GetComponent<Collider>();
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
        // TODO: Delete after testing
        Debug.Log($"{collider.name} grabbed a {this.gameObject.name} pickup");
        OnPickup();
    }

    public virtual void OnPickup()
    {
        Destroy(this.gameObject);
    }
}
