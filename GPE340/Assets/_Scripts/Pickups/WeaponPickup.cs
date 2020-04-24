using System.Collections;
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

    #region Public Properties

    #endregion

    // Awake is called before Start
    public override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {

    }
    public void OnTriggerEnter(Collider collider)
    {
        // Disable the trigger so that the code doesn't accidentally execute multiple times
        PickupCollider.isTrigger = false;
        // TODO: Delete after testing
        Debug.Log($"{collider.name} grabbed a {this.gameObject.name} pickup");
        OnPickup(collider);
    }

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

    public void OnPlayerPickup(Collider collider)
    {
        OnPlayerPickup(collider.GetComponent<PawnData>());
    }

    public void OnPlayerPickup(PawnData playerPawnData)
    {
        playerPawnData.InventoryMgr.AddWeaponToInventory(_weaponData);
    }
}
