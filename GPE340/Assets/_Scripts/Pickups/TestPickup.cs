using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPickup : Pickup
{
    #region Private Properties
#pragma warning disable CS0649

#pragma warning restore CS0649
    #endregion

    #region Public Properties

    #endregion

    public override void OnTriggerEnter(Collider collider)
    {
        
        // TODO: Delete after testing
        Debug.Log($"{collider.name} grabbed a {this.gameObject.name} pickup");
        OnPickup();
    }

    public override void OnPickup()
    {
        base.OnPickup();
    }
}
