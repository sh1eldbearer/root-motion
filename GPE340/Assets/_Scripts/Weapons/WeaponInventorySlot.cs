using System;
using UnityEngine;
using Utility.Enums;

[System.Serializable]
public class WeaponInventorySlot
{
    // TODO: Needs tooltips/summary tags
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip(""), SerializeField] private WeaponType _weaponType;
    [Tooltip(""), SerializeField] private WeaponData _weaponData;
    [Tooltip(""), SerializeField] private WeaponModelData _weaponModelData;
    [Tooltip(""), SerializeField] private int _currentAmmo;
    [Tooltip(""), SerializeField] private int _maxAmmo;

#pragma warning restore CS0649
    #endregion

    #region Public Properties
    public WeaponType WeaponType
    {
        get { return _weaponType; }
    }

    public WeaponData WeaponData
    {
        get { return _weaponData; }
    }

    public WeaponModelData WeaponModelData
    {
        get { return _weaponModelData; }
    }

    public int CurrentAmmo
    {
        get { return _currentAmmo; }
    }

    public int MaxAmmo
    {
        get { return _maxAmmo; }
    }

    #endregion

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
