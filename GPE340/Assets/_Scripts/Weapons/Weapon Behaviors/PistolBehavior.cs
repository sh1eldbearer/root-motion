using System.Collections;
using UnityEngine;
using Utility.Enums;

public class PistolBehavior : WeaponBehavior
{ 
    /// <summary>
    /// Fires this weapon.
    /// </summary>
    public override void Shoot()
    {
        // If the weapon is on cooldown, it won't fire
        if (_fireCooldownTimer <= 0f)
        {
            // Plays the weapon's muzzle flash particle effect
            _weaponModelData.PlayMuzzleFlash();

            // TODO: Use line renderer to generate a visual effect showing the path of the shot

            // Looks to see if the shot hits anything
            RaycastHit hitInfo;
            if (Physics.Raycast(_weaponModelData.RaycastOriginTransform.position,
                _weaponModelData.RaycastOriginTransform.forward, out hitInfo,
                _pawnData.InventoryMgr.EquippedWeaponRange))
            {
                // Checks to see if the object can take damage
                IDamageable damageable = hitInfo.collider.GetComponentInChildren<IDamageable>();

                if (damageable != null)
                {
                    // TODO: Remove debug.log later
                    Debug.Log(
                        $"{_pawnData.gameObject.name} shot {hitInfo.collider.name} with a {WeaponType.Pistol.ToString()} " +
                        $"for {_pawnData.InventoryMgr.EquippedWeaponDamage} damage.");

                    // Has the other object take damage
                    damageable.TakeDamage(_pawnData.InventoryMgr.EquippedWeaponDamage);
                }
            }

            // Starts the weapon's cooldown timer
            StartCoroutine(CooldownTimer());
        }
    }
}
