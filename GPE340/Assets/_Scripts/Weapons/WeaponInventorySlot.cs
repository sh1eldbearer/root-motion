using System;
using UnityEngine;
using Utility.Enums;

[System.Serializable]
public class WeaponInventorySlot
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The type of weapon to be stored in this inventory slot."), 
        SerializeField] private WeaponType _weaponType;
    [Tooltip("The WeaponData ScriptableObject stored in this inventory slot."), 
        SerializeField] private WeaponData _weaponData;
    [Tooltip("The WeaponModelData component associated with the weapon stored in this inventory slot."), 
        SerializeField] private WeaponModelData _weaponModelData;
    [Tooltip("Whether or not this weapon should ignore ammo counts when firing."),
        SerializeField] private bool _hasInfiniteAmmo;
    [Tooltip("The amount of ammo the pawn currently has for this weapon."), 
        SerializeField] private int _currentAmmo;
    [Tooltip("The maximum amount of ammo the pawn can carry for this weapon."), 
        SerializeField] private int _maxAmmo;

#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The type of weapon to be stored in this inventory slot.
    /// </summary>
    public WeaponType WeaponType
    {
        get { return _weaponType; }
    }

    /// <summary>
    /// The WeaponData ScriptableObject stored in this inventory slot.
    /// </summary>
    public WeaponData WeaponData
    {
        get { return _weaponData; }
    }

    /// <summary>
    /// The WeaponModelData component associated with the weapon stored in this inventory slot.
    /// </summary>
    public WeaponModelData WeaponModelData
    {
        get { return _weaponModelData; }
    }

    /// <summary>
    /// Whether or not this weapon should ignore ammo counts when firing.
    /// </summary>
    public bool HasInfiniteAmmo
    {
        get { return _hasInfiniteAmmo; }
    }

    /// <summary>
    /// The amount of ammo the pawn currently has for this weapon.
    /// </summary>
    public int CurrentAmmo
    {
        get { return _currentAmmo; }
    }

    /// <summary>
    /// The maximum amount of ammo the pawn can carry for this weapon.
    /// </summary>
    public int MaxAmmo
    {
        get { return _maxAmmo; }
    }
    #endregion

    /// <summary>
    /// Changes the WeaponData stored in this inventory slot.
    /// </summary>
    /// <param name="newInfo">The new WeaponData component to be stored in this inventory slot.</param>
    public void SetNewWeaponInfo(WeaponData newInfo)
    {
        _weaponData = newInfo;
    }

    /// <summary>
    /// Increases the weapon's current ammo count by 1.
    /// </summary>
    public void IncreaseCurrentAmmo()
    {
        _currentAmmo = Mathf.Clamp(_currentAmmo++, 0, _maxAmmo);
    }

    /// <summary>
    /// Increases the weapon's current ammo count by the specified amount.
    /// </summary>
    /// <param name="addAmount">The amount of ammo to add the weapon's current ammo count. Must be a positive,
    /// non-zero number.</param>
    public void IncreaseCurrentAmmo(int addAmount)
    {
        if (addAmount > 0)
        {
            _currentAmmo = Mathf.Clamp(_currentAmmo + addAmount, 0, _maxAmmo);
        }
        else
        {
            // If addAmount is zero or a negative value, throw an error
            throw new Exception($"You must use a positive, non-zero number with this method!");
        }
    }

    /// <summary>
    /// Decreases the weapon's current ammo count by 1.
    /// </summary>
    public void DecreaseCurrentAmmo()
    {
        _currentAmmo = Mathf.Clamp(_currentAmmo--, 0, _maxAmmo);
    }

    /// <summary>
    /// Decreases the weapon's current ammo count by the specified amount.
    /// </summary>
    /// <param name="subAmount">The amount of ammo to reduce the weapon's current ammo count by. Must be a
    /// positive, non-zero number.</param>
    public void DecreaseCurrentAmmo(int subAmount)
    {
        if (subAmount > 0)
        {
            _currentAmmo = Mathf.Clamp(_currentAmmo - subAmount, 0, _maxAmmo);
        }
        else
        {
            // If subAmount is zero or a negative value, throw an error
            throw new Exception($"You must use a positive, non-zero number with this method!");
        }
    }
}
