using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    /// <summary>
    /// Defines behaviors for when the pickup's trigger is entered.
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter(Collider collider);

    /// <summary>
    /// Defines behaviors when this object is picked up.
    /// </summary>
    void OnPickup(Collider collider);
}
