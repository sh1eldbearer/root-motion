using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Behavior for when the pickup object is touched by a pawn
    /// </summary>
    /// <param name="pawnData"></param>
    protected override void OnPickup(PawnData pawnData)
    {
        // TODO: Define PawnData class
        base.OnPickup(pawnData);
    }
}
