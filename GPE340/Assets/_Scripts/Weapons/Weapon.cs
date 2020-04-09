﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("Weapon Stats")]
    //[Tooltip(""), SerializeField] private WeaponScriptable _weaponData;
    [SerializeField] private Enums.WeaponQuality _weaponQuality = Enums.WeaponQuality.Base;

    [Header("Weapon Skins"), Tooltip("The skins to use for each quality tier of the weapon.")]
    [SerializeField] private Material _baseMat;
    [SerializeField] private Material _uncommonMat;
    [SerializeField] private Material _rareMat;
    [SerializeField] private Material _epicMat;
    [SerializeField] private Material _legendaryMat;

    [Tooltip("The list of parts to apply materials to when the weapon quality changes. " +
             "Will be populated at runtime if the parts are not manually added."),
        SerializeField] private List<MeshRenderer> _partMeshes = new List<MeshRenderer>();

    [Header("IK Positions"), 
        Tooltip("NOTE: These can be left null if they aren't needed for a particular weapon.")]
    [SerializeField] private Transform _lHandIKTransform;
    [SerializeField] private Transform _lElbowIKTransform;
    [SerializeField] private Transform _rHandIKTransform;
    [SerializeField] private Transform _rElbowIKTransform;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The transform to use for the avatar's left hand's position and rotation.
    /// </summary>
    public Transform LHandIKTransform
    {
        get { return _lHandIKTransform; }
    }

    /// <summary>
    /// The transform to use for the avatar's left elbow's position and rotation.
    /// </summary>
    public Transform LElbowIKTransform
    {
        get { return _lElbowIKTransform; }
    }

    /// <summary>
    /// The transform to use for the avatar's right hand's position and rotation.
    /// </summary>
    public Transform RHandIKTransform
    {
        get { return _rHandIKTransform; }
    }

    /// <summary>
    /// The transform to use for the avatar's right elbow's position and rotation.
    /// </summary>
    public Transform RElbowIKTransform
    {
        get { return _rElbowIKTransform; }
    }
    #endregion

    // Awake is called before Start
    protected virtual void Awake()
    {
        // Stores the MeshRenderers of each part of the weapon so we can modify their materials
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
        if (_weaponQuality != Enums.WeaponQuality.Base)
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
        if (_weaponQuality != Enums.WeaponQuality.Legendary)
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
    public void ChangeSkin(Enums.WeaponQuality weaponQuality)
    {
        switch (weaponQuality)
        {
            case Enums.WeaponQuality.Base:
                SetToBaseQuality();
                break;
            case Enums.WeaponQuality.Uncommon:
                SetToUncommonQuality();
                break;
            case Enums.WeaponQuality.Rare:
                SetToRareQuality();
                break;
            case Enums.WeaponQuality.Epic:
                SetToEpicQuality();
                break;
            case Enums.WeaponQuality.Legendary:
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