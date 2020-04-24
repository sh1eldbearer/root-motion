using System.Collections;
using System.Collections.Generic;
using Utility.Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "New Weapon Data (ScriptableObject)", order = 230)]
public class WeaponData : ScriptableObject
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The type of weapon this is."),
        SerializeField] private WeaponType _weaponType = WeaponType.None;
    [Tooltip("The quality level of this weapon."), 
        SerializeField] private WeaponQuality _weaponQuality = WeaponQuality.Base;
    [Tooltip("The amount of damage dealt per shot by this weapon."),
        SerializeField] private float _baseDamage = 10;
    [Tooltip("The delay between effective pulls of the trigger for this weapon (in seconds)."),
        SerializeField] private float _baseFireRate = 0.75f; // TODO: Implement a range after initial testing
    [Tooltip("The clip size of this weapon."),
        SerializeField] private float _baseClipSize = 6;
    [Tooltip("The firing behavior of this weapon."),
        SerializeField] private FireMode _firingMode = FireMode.SingleShot;

    [Header("Burst Settings (Ignored if not using BurstFire mode)")]
    [Tooltip("How many bullets should be fired per trigger pull in burst mode. (Will be ignored if " +
             "the weapon is not configured to use BurstFire mode.)"),
        SerializeField] private int _burstSize = 1;
    [Tooltip("The delay between shots when the weapon is in burst mode. (Will be ignored if the " +
             "weapon is not configured to use BurstFire mode."),
        SerializeField] private float _burstDelay = 0.25f; // TODO: Implement a range after initial testing
#pragma warning restore CS0649
    #endregion

    #region Public Properties

    /// <summary>
    /// The type of weapon this is.
    /// </summary>
    public WeaponType WeaponType
    {
        get { return _weaponType; }
    }

    /// <summary>
    /// The quality level of this weapon.
    /// </summary>
    public WeaponQuality Quality
    {
        get { return _weaponQuality; }
    }

    /// <summary>
    /// The amount of damage dealt per shot by this weapon.
    /// </summary>
    public float BaseDamage
    {
        get { return _baseDamage; }
    }

    /// <summary>
    /// The delay between effective pulls of the trigger for this weapon (in seconds). 
    /// </summary>
    public float BaseFireRate
    {
        get { return _baseFireRate; }
    }

    /// <summary>
    /// The clip size of this weapon.
    /// </summary>
    public float BaseClipSize
    {
        get { return _baseClipSize; }
    }

    /// <summary>
    /// The firing behavior of this weapon.
    /// </summary>
    public FireMode FiringMode
    {
        get { return _firingMode; }
    }

    /// <summary>
    /// How many bullets should be fired per trigger pull in burst mode. (Will be ignored if 
    /// the weapon is not configured to use BurstFire mode!)
    /// </summary>
    public int BurstSize
    {
        get { return _burstSize; }
    }

    /// <summary>
    /// The delay between shots when the weapon is in burst mode. (Will be ignored if the
    /// weapon is not configured to use BurstFire mode.
    /// </summary>
    public float BurstDelay
    {
        get { return _burstDelay; }
    }
    #endregion
}
