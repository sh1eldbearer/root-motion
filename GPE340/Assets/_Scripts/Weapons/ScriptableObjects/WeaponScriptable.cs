using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New WeaponScriptable", menuName = "Weapon ScriptableObject", order = 230)]
public class WeaponScriptable : ScriptableObject
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The display name of this weapon."),
        SerializeField] private string _name = "New Weapon";
    [Tooltip("The quality level of this weapon."), 
        SerializeField] private Enums.WeaponQuality _weaponQuality = Enums.WeaponQuality.Base;
    [Tooltip("The amount of damage dealt per shot by this weapon."),
        SerializeField] private float _baseDamage = 10;
    [Tooltip("The delay between effective pulls of the trigger for this weapon (in seconds)."),
        SerializeField] private float _baseFireRate = 0.75f; // TODO: Implement a range after initial testing
    [Tooltip("The clip size of this weapon."),
        SerializeField] private float _baseClipSize = 6;
    [Tooltip("The firing behavior of this weapon."),
        SerializeField] private Enums.FireMode _firingMode = Enums.FireMode.SingleShot;

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
    /// The display name of this weapon.
    /// </summary>
    public string Name
    {
        get { return _name; }
    }

    /// <summary>
    /// The quality level of this weapon.
    /// </summary>
    public Enums.WeaponQuality Quality
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
    public Enums.FireMode FiringMode
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
