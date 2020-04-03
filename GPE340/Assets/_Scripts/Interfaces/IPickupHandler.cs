using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupHandler
{
    /// <summary>
    /// Defines behaviors when this object is picked up.
    /// </summary>
    void OnPickup();
}
