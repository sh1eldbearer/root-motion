using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    /// <summary>
    /// Defines behaviors when this object is picked up.
    /// </summary>
    void OnPickup(Collider collider);
}
