using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // TODO: Tooltips and summary tags
    #region Private Properties
#pragma warning disable CS0649
    [SerializeField, Range(0, 3)] private int _equippedWeaponIndex = 0;
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
        
    }

    public WeaponInventorySlot GetEquippedWeapon()
    {
        return _weaponInventory[_equippedWeaponIndex];
    }
}
