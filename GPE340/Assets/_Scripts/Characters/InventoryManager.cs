using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Enums;

public class InventoryManager : MonoBehaviour
{
    #region Constants
#pragma warning disable CS0649
    // Constants for the number of weapons
    private const int MIN_WEAPON_COUNT = 0;
    private const int MAX_WEAPON_COUNT = 3;
    #endregion

    // TODO: Tooltips and summary tags
    #region Private Properties
#pragma warning disable CS0649
    [SerializeField, Range(MIN_WEAPON_COUNT, MAX_WEAPON_COUNT)] private int _equippedWeaponIndex = 0;
    [SerializeField] private List<WeaponInventorySlot> _weaponInventory = new List<WeaponInventorySlot>(4);
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// 
    /// </summary>
    public int EquippedWeaponIndex
    {
        get { return _equippedWeaponIndex; }
    }
    #endregion

    // Awake is called before Start
    private void Awake()
	{
		
	}

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: Move functionality elsewhere
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            PreviousWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Quote))
        {
            NextWeapon();
        }
    }

    public WeaponInventorySlot GetEquippedWeapon()
    {
        return _weaponInventory[_equippedWeaponIndex];
    }

    // I would have liked to use an enumerator for the weapon swapping functions, but C# enumerators can't
    // iterate backwards through collections, and writing my own custom enumerator class isn't a time-saving
    // activity at present.


    public void NextWeapon()
    {
        int nextIndex =_equippedWeaponIndex + 1;

        while (nextIndex != _equippedWeaponIndex)
        {
            // Checks to make sure the index is within a valid range
            if (nextIndex > MAX_WEAPON_COUNT)
            {
                // If not, resets the index to the first index
                nextIndex = MIN_WEAPON_COUNT;
            }

            // If the element being checked is the same as when we started, terminate the method
            if (nextIndex == _equippedWeaponIndex)
            {
                return;
            }
            // If the element being checked has weapon data, equip that weapon
            else if (_weaponInventory[nextIndex].WeaponInfo != null)
            {
                ToggleWeaponTransforms(nextIndex);
                SetEquippedWeaponIndex(nextIndex);
                return;
            }
            // Otherwise, move on checking the to the next inventory slot
            else
            {
                nextIndex++;
            }
        }
    }

    public void PreviousWeapon()
    {
        int nextIndex = _equippedWeaponIndex - 1;

        while (nextIndex != _equippedWeaponIndex)
        {
            // Checks to make sure the index is within a valid range
            if (nextIndex < MIN_WEAPON_COUNT)
            {
                // If not, resets the index to the first index
                nextIndex = MAX_WEAPON_COUNT;
            }

            // If the element being checked is the same as when we started, terminate the method
            if (nextIndex == _equippedWeaponIndex)
            {
                return;
            }
            // If the element being checked has weapon data, equip that weapon
            else if (_weaponInventory[nextIndex].WeaponInfo != null)
            {
                ToggleWeaponTransforms(nextIndex);
                SetEquippedWeaponIndex(nextIndex);
                return;
            }
            // Otherwise, move on checking the to the next inventory slot
            else
            {
                nextIndex--;
            }
        }
    }

    private void ToggleWeaponTransforms(int indexToShow)
    {
        for (int loopIndex = 0; loopIndex <= MAX_WEAPON_COUNT; loopIndex++)
        {
            if (loopIndex == indexToShow)
            {
                _weaponInventory[loopIndex].WeaponModelData.transform.gameObject.SetActive(true);
            }
            else
            {
                _weaponInventory[loopIndex].WeaponModelData.transform.gameObject.SetActive(false);
            }
        }
    }

    private void SetEquippedWeaponIndex(int targetIndex)
    {
        _equippedWeaponIndex = targetIndex;
    }

    private void SetWeaponData(WeaponData pickupData, int slotIndex)
    {
        _weaponInventory[slotIndex].SetNewWeaponInfo(pickupData);

        SetEquippedWeaponIndex(slotIndex);

        _weaponInventory[slotIndex].WeaponModelData.ChangeSkin(pickupData.Quality);
        // TODO: Additional functionality
    }

    public void AddWeaponToInventory(WeaponData pickupData)
    {
        int targetIndex = FindWeaponSlotByType(pickupData.WeaponType);

        // Player does not have a weapon of the pickup type
        if (_weaponInventory[targetIndex].WeaponInfo == null)
        {
            // Add weapon to inventory
            SetWeaponData(pickupData, targetIndex);

            // Add ammo to weapon
            _weaponInventory[targetIndex].IncreaseCurrentAmmo(pickupData.BaseClipSize * 4); // TODO: Make the multiplier a variable somewhere

            ToggleWeaponTransforms(targetIndex);
        }
        // The picked up weapon has a higher quality than the player's previous version of the weapon
        else if (_weaponInventory[targetIndex].WeaponInfo.Quality < pickupData.Quality)
        {
            // Add weapon to inventory
            SetWeaponData(pickupData, targetIndex);

            // Add ammo to weapon
            _weaponInventory[targetIndex].IncreaseCurrentAmmo(pickupData.BaseClipSize * 2); // TODO: Make the multiplier a variable somewhere

            ToggleWeaponTransforms(targetIndex);
        }
        // The player already has an equal or higher-quality version of this weapon
        {
            // Add ammo to weapon
            _weaponInventory[targetIndex].IncreaseCurrentAmmo(pickupData.BaseClipSize * 2); // TODO: Make the multiplier a variable somewhere
        }
    }


    /// <summary>
    /// Looks through the inventory for the weapon of the 
    /// </summary>
    /// <param name="compareType"></param>
    /// <returns></returns>
    private int FindWeaponSlotByType(WeaponType compareType)
    {
        foreach (WeaponInventorySlot weaponSlot in _weaponInventory)
        {
            if (weaponSlot.WeaponType == compareType)
            {
                return _weaponInventory.IndexOf(weaponSlot);
            }
        }

        // If there's no match, I forgot to assign weapon types in the inventory, so throw an exception
        throw new NullReferenceException($"No inventory slot accepts a weapon of type {compareType.ToString()}. " +
                                         $"Check the way your players' Weapon Inventories are configured.");
    }
}
