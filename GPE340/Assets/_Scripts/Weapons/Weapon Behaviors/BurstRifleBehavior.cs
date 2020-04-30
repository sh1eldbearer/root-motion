using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstRifleBehavior : WeaponBehavior
{
    /// <summary>
    /// Fires this weapon.
    /// </summary>
    public override void Shoot()
    {
        // If the weapon is on cooldown, it won't fire
        if (_fireCooldownTimer <= 0f)
        {
            StartCoroutine(BurstFire());

            // Starts the weapon's cooldown timer
            StartCoroutine(CooldownTimer());
        }
    }

    /// <summary>
    /// Fires multiple shots from this weapon within a short window
    /// </summary>
    /// <returns>Coroutine.</returns>
    public IEnumerator BurstFire()
    {
        float burstTimer = _pawnData.InventoryMgr.EquippedWeaponBurstDuration /
                           _pawnData.InventoryMgr.EquippedWeaponBurstSize;

        for (int burstCount = 0; burstCount <= _pawnData.InventoryMgr.EquippedWeaponBurstSize; burstCount++)
        {
            base.Shoot();
            yield return new WaitForSeconds(burstTimer);
        }
    }
}
