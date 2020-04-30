﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBehavior : WeaponBehavior
{
    /// <summary>
    /// Fires this weapon.
    /// </summary>
    public override void Shoot()
    {
        // If the weapon is on cooldown, it won't fire
        if (_fireCooldownTimer <= 0f)
        {
            base.Shoot();

            // Starts the weapon's cooldown timer
            StartCoroutine(CooldownTimer());
        }
    }
}
