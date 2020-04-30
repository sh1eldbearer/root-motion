using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup, IPlayerPickup, IEnemyPickup
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The amount of health to restore to a pawn when this powerup is picked up."),
        SerializeField] private float _healAmount = 25f;
#pragma warning restore CS0649
    #endregion

    public void OnTriggerEnter(Collider collider)
    {
        // Disable the trigger so that the code doesn't accidentally execute multiple times
        PickupCollider.isTrigger = false;
        OnPickup(collider);
    }

    /// <summary>
    /// Defines pickup behavior for this pickup object.
    /// </summary>
    /// <param name="collider">The collider of the object that collided with this object.</param>
    public void OnPickup(Collider collider)
    {
        if (collider.tag == "Player")
        {
            OnPlayerPickup(collider);
            Destroy(this.gameObject);
        }
        else if (collider.tag == "Enemy")
        {
            OnEnemyPickup(collider);
            Destroy(this.gameObject);
        }
        else
        {
            // Re-enables the trigger collider
            PickupCollider.isTrigger = true;
        }
    }

    /// <summary>
    /// Further defines pickup behavior when this object is picked up by a player pawn.
    /// </summary>
    /// <param name="collider">The collider of the object that collided with this object.</param>
    public void OnPlayerPickup(Collider collider)
    {
        OnPlayerPickup(collider.GetComponent<PawnData>());
    }

    /// <summary>
    /// Further defines pickup behavior when this object is picked up by a player pawn.
    /// </summary>
    /// <param name="playerPawnData">The PawnData component of the object that collided with this object.</param>
    public void OnPlayerPickup(PawnData playerPawnData)
    {
        playerPawnData.HealthMgr.ReceiveHealing(_healAmount);
    }

    /// <summary>
    /// Further defines pickup behavior when this object is picked up by an enemy pawn.
    /// </summary>
    /// <param name="collider">The collider of the object that collided with this object.</param>
    public void OnEnemyPickup(Collider collider)
    {
        OnEnemyPickup(collider.GetComponent<PawnData>());
    }

    /// <summary>
    /// Further defines pickup behavior when this object is picked up by an enemy pawn.
    /// </summary>
    /// <param name="playerPawnData">The PawnData component of the object that collided with this object.</param>
    public void OnEnemyPickup(PawnData enemyPawnaData)
    {
        enemyPawnaData.HealthMgr.ReceiveHealing(_healAmount);
    }
}
