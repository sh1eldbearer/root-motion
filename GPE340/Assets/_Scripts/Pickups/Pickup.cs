using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Pickup : MonoBehaviour
{
    /* Public Variables */
    public float rotateSpeed = 360.0f;
    [Range(1f, 10f)] public float lifeSpan = 5.0f;

    /* Private Variables */
    private Transform tf;
    

    // Start is called before the first frame update
    protected virtual void Start()
    {
        /*  Component reference assignments */
        tf = this.GetComponent<Transform>();

        // Make sure this object always has an active trigger collider
        GetComponent<Collider>().isTrigger = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        SpinPickup();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        // Looks for a PawnData-type component on the other object
        PawnData pawnData = other.GetComponent<PawnData>();

        // If there is a PawnData component, run the OnPickup functionality
        if (pawnData != null)
        {
            OnPickup(pawnData);
        }
        else
        {
            // Otherwise, ignore the collision
        }
    }

    /// <summary>
    /// Rotates the pickup object in the game world, in order to draw attention to it.
    /// </summary>
    protected void SpinPickup()
    {
        tf.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    /// <summary>
    /// Behavior for when the pickup object is touched by a pawn
    /// </summary>
    /// <param name="pawnData"></param>
    protected virtual void OnPickup(PawnData pawnData)
    {
        // TODO: Apply pickup to pawn

        // Destry the pickup after it is picked up
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Destroys this pickup object.
    /// </summary>
    protected void DestroyPickup()
    {
        Destroy(this.gameObject);
    }
}
