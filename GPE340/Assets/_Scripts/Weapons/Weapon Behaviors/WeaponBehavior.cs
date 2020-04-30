﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehavior : MonoBehaviour, IShootable
{
    #region Protected Properties

#pragma warning disable CS0649
    [Tooltip("The PawnData component for this pawn."),
        SerializeField] protected PawnData _pawnData;

    [Tooltip("The WeaaponModelComponent for this weapon."),
        SerializeField] protected WeaponModelData _weaponModelData;

    [Tooltip("The timer for the firing cooldown of this weapon."),
        Space, SerializeField] protected float _fireCooldownTimer = 0f;
#pragma warning restore CS0649

    #endregion

    // Awake is called before Start
    protected virtual void Awake()
    {
        // Component reference assignments
        if (_pawnData == null)
        {
            _pawnData = this.gameObject.GetComponentInParent<PawnData>();
        }

        if (_weaponModelData == null)
        {
            _weaponModelData = this.gameObject.GetComponent<WeaponModelData>();
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public virtual void Shoot()
    {

    }

    /// <summary>
    /// Decreases this weapon's cooldown timer until it reaches zero (or lower).
    /// </summary>
    /// <returns>Coroutine.</returns>
    protected IEnumerator CooldownTimer()
    {
        // Initializes the cooldown timer.
        _fireCooldownTimer = 1f / _pawnData.InventoryMgr.EquippedWeaponFireRate;

        while (_fireCooldownTimer > 0)
        {
            // Using smoothDeltaTime seems to have more reliable results in regards to preventing players from firing
            // more often than inteded
            _fireCooldownTimer -= Time.smoothDeltaTime;
            yield return null;
        }
    }
}