using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class WeaponInventorySlot
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip(""), SerializeField] private Enums.WeaponType _weaponType;
    [Tooltip(""), SerializeField] private Transform _weaponTransform;
    [Tooltip(""), SerializeField] private WeaponData _weaponInfo;

#pragma warning restore CS0649
    #endregion

    #region Public Properties
    public Enums.WeaponType WeaponType
    {
        get { return _weaponType; }
    }

    public Transform WeaponTransform
    {
        get { return _weaponTransform; }
    }

    public WeaponData WeaponInfo
    {
        get { return _weaponInfo; }
    }
    #endregion

    public void SetNewWeaponInfo(WeaponData newInfo)
    {
        _weaponInfo = newInfo;
    }
}
