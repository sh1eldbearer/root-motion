using UnityEngine;
using Utility.Enums;

public class PistolBehavior : MonoBehaviour, IWeapon, IShootable
{
    // TODO: commenting pass
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The PawnData component for this pawn."),
        SerializeField] private PawnData _pawnData;
    private WeaponModelData _weaponModelData;
#pragma warning restore CS0649
    #endregion

    #region Public Properties

    #endregion
	
	// Awake is called before Start
	private void Awake()
	{
		// Component reference assignments
        if (_pawnData == null)
        {
            _pawnData = this.gameObject.GetComponentInParent<PawnData>();
        }
        if (_weaponModelData == null)
        {
            _weaponModelData = this.gameObject.GetComponent<WeaponModelData>();
        }
	}

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Shoot()
    {
        int weaponIndex = _pawnData.InventoryMgr.FindWeaponSlotByType(WeaponType.Pistol);

        RaycastHit hitInfo;
        if (Physics.Raycast(_weaponModelData.RaycastOriginTransform.position,
            _weaponModelData.RaycastOriginTransform.forward, out hitInfo, _pawnData.InventoryMgr.EquippedWeaponRange))
        {
            IDamageable damageable = hitInfo.collider.GetComponentInChildren<IDamageable>();

            if (damageable != null)
            {
                Debug.Log($"{_pawnData.gameObject.name} shot {hitInfo.collider.name} with a {WeaponType.Pistol.ToString()} " +
                          $"for {_pawnData.InventoryMgr.EquippedWeaponDamage} damage.");
                damageable.TakeDamage(_pawnData.InventoryMgr.EquippedWeaponDamage);
            }
        }
    }
}
