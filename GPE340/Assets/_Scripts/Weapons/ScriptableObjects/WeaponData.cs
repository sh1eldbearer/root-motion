using Utility.Enums;
using UnityEngine;

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
    [Tooltip("The number of bullets per second this weapon can fire."),
        SerializeField] private float _baseFireRate = 0.75f; // TODO: Implement a range after initial testing?
    [Tooltip("The clip size of this weapon."),
        SerializeField] private int _baseClipSize = 6;
    [Tooltip("The firing behavior of this weapon."),
        SerializeField] private FireMode _firingMode = FireMode.SingleShot;
    [Tooltip("The effective range of this weapon."),
        SerializeField] private float _baseRange = 50f;

    [Header("Burst Settings (Ignored if not using BurstFire mode)")]
    [Tooltip("How many bullets should be fired per trigger pull in burst mode. (Will be ignored if " +
             "the weapon is not configured to use BurstFire mode.)"),
        SerializeField] private int _burstSize = 1;
    [Tooltip("The length of time it takes for a burst to be fired. (Will be ignored if the " +
             "weapon is not configured to use BurstFire mode.)"),
        SerializeField] private float _burstDuration = 0.25f; // TODO: Implement a range after initial testing?
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
    /// The number of bullets per second this weapon can fire. 
    /// </summary>
    public float BaseFireRate
    {
        get { return _baseFireRate; }
    }

    /// <summary>
    /// The clip size of this weapon.
    /// </summary>
    public int BaseClipSize
    {
        get { return _baseClipSize; }
    }

    /// <summary>
    /// The effective range of this weapon.
    /// </summary>
    public float BaseRange
    {
        get { return _baseRange; }
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
    /// The length of time it takes for a burst to be fired. (Will be ignored if the
    /// weapon is not configured to use BurstFire mode.)
    /// </summary>
    public float BurstDuration
    {
        get { return _burstDuration; }
    }

    #endregion
}
