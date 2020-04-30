﻿using System.Collections;
using System.Collections.Generic;
using Utility.Enums;
using UnityEngine;

public class WeaponPickup : Pickup, IPlayerPickup
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The data about the weapon this pickup represents."),
        SerializeField] private WeaponData _weaponData;
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
        else
        {
            // Re-enables the trigger collider, enemies won't use weapon pickups
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
        playerPawnData.InventoryMgr.AddWeaponToInventory(_weaponData);
    }
}
