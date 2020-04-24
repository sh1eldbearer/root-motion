using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPickup : Pickup, IPlayerPickup
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
        OnPickup(collider);
    }

    public void OnPickup(Collider collider)
    {
        base.OnPickup(collider);
    }

    public void OnPlayerPickup(PawnData playerPawn)
    {
        Debug.Log("A player picked me up");
    }
}
