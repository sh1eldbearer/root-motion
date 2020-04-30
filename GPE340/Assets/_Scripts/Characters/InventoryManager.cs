using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Enums;

public class InventoryManager : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The index of the currently equipped weapon in this weapon inventory."),
        SerializeField] private int _equippedWeaponIndex = 0;
    [Tooltip("The list of weapons this pawn currently has."),
        SerializeField] private List<WeaponInventorySlot> _weaponInventory = new List<WeaponInventorySlot>(4);
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The index of the currently equipped weapon in this weapon inventory.
    /// </summary>
    public int EquippedWeaponIndex
    {
        get { return _equippedWeaponIndex; }
    }

    public WeaponModelData EquippedWeaponModelData
    {
        get { return GetEquippedWeapon().WeaponModelData; }
    }

    /// <summary>
    /// The type of weapon currently equipped by this pawn.
    /// </summary>
    public WeaponType EquippedWeaponType
    {
        get { return GetEquippedWeapon().WeaponType; }
    }

    /// <summary>
    /// The quality level of the weapon currently equipped by this pawn.
    /// </summary>
    public WeaponQuality EquippedWeaponQuality
    {
        get { return GetEquippedWeapon().WeaponData.Quality; }
    }

    /// <summary>
    /// The amount of damage dealt per shot by this the currently equipped weapon.
    /// </summary>
    public float EquippedWeaponDamage
    {
        get { return GetEquippedWeapon().WeaponData.BaseDamage; }
    }

    /// <summary>
    /// The delay between effective pulls of the trigger for the currently equipped weapon (in seconds). 
    /// </summary>
    public float EquippedWeaponFireRate
    {
        get { return GetEquippedWeapon().WeaponData.BaseFireRate; }
    }

    /// <summary>
    /// The clip size of the currently equipped weapon.
    /// </summary>
    public int EquippedWeaponClipSize
    {
        get { return GetEquippedWeapon().WeaponData.BaseClipSize; }
    }

    /// <summary>
    /// The effective range of the weapon currently equipped weapon.
    /// </summary>
    public float EquippedWeaponRange
    {
        get { return GetEquippedWeapon().WeaponData.BaseRange; } 
    }

    /// <summary>
    /// The firing behavior of the currently equipped weapon.
    /// </summary>
    public FireMode EquippedWeaponFiringMode
    {
        get { return GetEquippedWeapon().WeaponData.FiringMode; }
    }

    /// <summary>
    /// How many bullets should be fired by the currently equipped weapon per trigger pull
    /// in burst mode. (Will be ignored if the weapon is not configured to use BurstFire mode!)
    /// </summary>
    public int EquippedWeaponBurstSize
    {
        get { return GetEquippedWeapon().WeaponData.BurstSize; }
    }

    /// <summary>
    /// The delay between shots when the currently weapon is in burst mode. (Will be ignored if the
    /// weapon is not configured to use BurstFire mode.
    /// </summary>
    public float EquippedWeaponBurstDelay
    {
        get { return GetEquippedWeapon().WeaponData.BurstDelay; }
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
        // TODO: Move functionality to player controller
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            PreviousWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Quote))
        {
            NextWeapon();
        }
    }

    /// <summary>
    /// Returns the inventory slot of the currently equipped weapon, in order to retrieve information
    /// about the weapon stored in that inventory slot.
    /// </summary>
    /// <returns></returns>
    private WeaponInventorySlot GetEquippedWeapon()
    {
        return _weaponInventory[_equippedWeaponIndex];
    }

    /// <summary>
    /// Tells the currently equipped weapon to shoot.
    /// </summary>
    public void ShootEquippedWeapon()
    {
        if (GetEquippedWeapon().WeaponModelData.WeaponBehavior != null)
        {
            GetEquippedWeapon().WeaponModelData.WeaponBehavior.Shoot();
        }
    }

    /// <summary>
    /// Switches to the next available weapon in the inventory list.
    /// </summary>
    public void NextWeapon()
    {
        int nextIndex =_equippedWeaponIndex + 1;

        while (nextIndex != _equippedWeaponIndex)
        {
            // Checks to make sure the index is within a valid range
            if (nextIndex == _weaponInventory.Count)
            {
                // If not, resets the index to the first index
                nextIndex = 0;
            }

            // If the element being checked is the same as when we started, terminate the method
            if (nextIndex == _equippedWeaponIndex)
            {
                return;
            }
            // If the element being checked has weapon data, equip that weapon
            else if (_weaponInventory[nextIndex].WeaponData != null)
            {
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

    /// <summary>
    /// Switches to the previous available weapon in the inventory list.
    /// </summary>
    public void PreviousWeapon()
    {
        int nextIndex = _equippedWeaponIndex - 1;

        while (nextIndex != _equippedWeaponIndex)
        {
            // Checks to make sure the index is within a valid range
            if (nextIndex < 0)
            {
                // If not, resets the index to the first index
                nextIndex = _weaponInventory.Count;
            }

            // If the element being checked is the same as when we started, terminate the method
            if (nextIndex == _equippedWeaponIndex)
            {
                return;
            }
            // If the element being checked has weapon data, equip that weapon
            else if (_weaponInventory[nextIndex].WeaponData != null)
            {
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
    
    /// <summary>
    /// Changes the active status of a pawn's weapon models so that the equipped weapon is the
    /// only active transform.
    /// </summary>
    private void ToggleWeaponTransforms()
    {
        for (int loopIndex = 0; loopIndex < _weaponInventory.Count; loopIndex++)
        {
            if (loopIndex == _equippedWeaponIndex)
            {
                _weaponInventory[loopIndex].WeaponModelData.transform.gameObject.SetActive(true);
            }
            else
            {
                _weaponInventory[loopIndex].WeaponModelData.transform.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Changes the index of the currently active weapon, and sets that weapon model as the active model
    /// </summary>
    /// <param name="targetIndex">The index of the element in the weapon inventory array to be used as
    /// the new equipped weapon.</param>
    private void SetEquippedWeaponIndex(int targetIndex)
    {
        _equippedWeaponIndex = targetIndex;
        ToggleWeaponTransforms();
    }

    /// <summary>
    /// Changes the weapon data stored in the designated inventory slot (used when a pawn picks up a new
    /// or higher quality weapon.)
    /// </summary>
    /// <param name="newData">The new weapon data to be stored in the pawn's inventory.</param>
    /// <param name="slotIndex">The index of the inventory array to be used.</param>
    private void SetWeaponData(WeaponData newData, int slotIndex)
    {
        _weaponInventory[slotIndex].SetNewWeaponInfo(newData);
        
        _weaponInventory[slotIndex].WeaponModelData.ChangeSkin(newData.Quality);
        // TODO: Additional functionality?
    }

    /// <summary>
    /// Compares the weapon type and quality of a picked up weapon against the pawn's current
    /// inventory, and adds or changes the data component stored in inventory if needed. (Also
    /// adds some ammo for that weapon type as well.)
    /// </summary>
    /// <param name="pickupData">The WeaponData stored in the pickup object.</param>
    public void AddWeaponToInventory(WeaponData pickupData)
    {
        int targetIndex = FindWeaponSlotByType(pickupData.WeaponType);

        // Player does not have a weapon of the pickup type
        if (_weaponInventory[targetIndex].WeaponData == null)
        {
            // Add weapon to inventory
            SetWeaponData(pickupData, targetIndex);

            // Add ammo to weapon
            _weaponInventory[targetIndex].IncreaseCurrentAmmo(pickupData.BaseClipSize * 4); // TODO: Make the multiplier a variable somewhere

            // Makes the new weapon the currently active weapon
            SetEquippedWeaponIndex(targetIndex);
        }
        // The picked up weapon has a higher quality than the player's previous version of the weapon
        else if (_weaponInventory[targetIndex].WeaponData.Quality < pickupData.Quality)
        {
            // Add weapon to inventory
            SetWeaponData(pickupData, targetIndex);

            // Add ammo to weapon
            _weaponInventory[targetIndex].IncreaseCurrentAmmo(pickupData.BaseClipSize * 2); // TODO: Make the multiplier a variable somewhere

            // Makes the new weapon the currently active weapon
            SetEquippedWeaponIndex(targetIndex);
        }
        // The player already has an equal or higher-quality version of this weapon
        {
            // Add ammo to weapon
            _weaponInventory[targetIndex].IncreaseCurrentAmmo(pickupData.BaseClipSize * 2); // TODO: Make the multiplier a variable somewhere
        }
    }


    /// <summary>
    /// Looks through the inventory for the weapon of the provided type.
    /// </summary>
    /// <param name="compareType">The type of weapon being looked for.</param>
    /// <returns>The index number of the weapon matching the provided compare type.</returns>
    public int FindWeaponSlotByType(WeaponType compareType)
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
