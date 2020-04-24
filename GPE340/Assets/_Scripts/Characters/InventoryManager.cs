using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [SerializeField] private List<WeaponInventorySlot> _weaponInventory = new List<WeaponInventorySlot>(4);
#pragma warning restore CS0649
    #endregion

    #region Public Properties

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
}
