using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("Weapon Stats")]
    //[Tooltip(""), SerializeField] private WeaponScriptable _weaponData;
    [SerializeField] private WeaponQuality _weaponQuality = WeaponQuality.Base;

    [Header("Weapon Skins"), Tooltip("The skins to use for each quality tier of the weapon.")]
    [SerializeField] private Material _baseMat;
    [SerializeField] private Material _uncommonMat;
    [SerializeField] private Material _rareMat;
    [SerializeField] private Material _epicMat;
    [SerializeField] private Material _legendaryMat;

    [Tooltip("The list of parts to apply materials to when the weapon quality changes. " +
             "Will be populated at runtime if the parts are not manually added."),
        SerializeField] private List<MeshRenderer> _partMeshes;

    [Header("IK Positions")]
    [SerializeField] private Transform _leftHandIKTransform;
    [SerializeField] private Transform _rightHandIKTransform;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    public Transform LeftHandIKTransform
    {
        get { return _leftHandIKTransform; }
    }

    public Transform RightHandIKTransform
    {
        get { return _rightHandIKTransform; }
    }

    #endregion

    // Awake is called before Start
    protected virtual void Awake()
    {
        // Stores the MeshRenders of each part of the weapon so we can modify their materials
        _partMeshes = this.gameObject.GetComponentsInChildren<MeshRenderer>().ToList();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // TODO: Remove after implementing in game
        SetToBaseQuality();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // TODO: Remove before submitting
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            DegradeWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Period))
        {
            UpgradeWeapon();
        }
    }

    /// <summary>
    /// Decreases the quality of the weapon to the next lowest-tier.
    /// </summary>
    private void DegradeWeapon()
    {
        if (_weaponQuality != WeaponQuality.Base)
        {
            _weaponQuality--;
            ChangeSkin(_weaponQuality);
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Increases the quality of the weapon to the next highest-tier.
    /// </summary>
    private void UpgradeWeapon()
    {
        if (_weaponQuality != WeaponQuality.Legendary)
        {
            _weaponQuality++;
            ChangeSkin(_weaponQuality);
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Changes the weapon's materials based on the weapon quality.
    /// </summary>
    /// <param name="weaponQuality">The quality of the weapon used to determine which
    /// weapon material to show.</param>
    public void ChangeSkin(WeaponQuality weaponQuality)
    {
        switch (weaponQuality)
        {
            case WeaponQuality.Base:
                SetToBaseQuality();
                break;
            case WeaponQuality.Uncommon:
                SetToUncommonQuality();
                break;
            case WeaponQuality.Rare:
                SetToRareQuality();
                break;
            case WeaponQuality.Epic:
                SetToEpicQuality();
                break;
            case WeaponQuality.Legendary:
                SetToLegendaryQuality();
                break;
        }
    }

    /// <summary>
    /// Sets the weapon to the base quality skins.
    /// </summary>
    private void SetToBaseQuality()
    {
        SetMaterials(_baseMat);
    }

    /// <summary>
    /// Sets the weapon to the uncommon quality skins.
    /// </summary>
    private void SetToUncommonQuality()
    {
        SetMaterials(_uncommonMat);
    }

    /// <summary>
    /// Sets the weapon to the rare quality skins.
    /// </summary>
    private void SetToRareQuality()
    {
        SetMaterials(_rareMat);
    }

    /// <summary>
    /// Sets the weapon to the epic quality skins.
    /// </summary>
    private void SetToEpicQuality()
    {
        SetMaterials(_epicMat);
    }

    /// <summary>
    /// Sets the weapon to the legendary quality skins.
    /// </summary>
    private void SetToLegendaryQuality()
    {
        SetMaterials(_legendaryMat);
    }

    /// <summary>
    /// Changes the materials currently being used by this weapon.
    /// </summary>
    /// <param name="newMaterial">The material to apply to the weapon model.</param>
    private void SetMaterials(Material newMaterial)
    {
#if UNITY_EDITOR
        // Lets me know if I forget to set a material (not that a bright pink material isn't obvious enough)
        if (newMaterial == null)
        {
            Debug.Log($"{this.gameObject.name} does not have a material set for the {_weaponQuality.ToString()} " +
                      $"quality!");
        }
#endif

        // Sets the material for each part of the weapon model
        foreach (MeshRenderer part in _partMeshes)
        {
            part.material = newMaterial;
        }
    }
}
