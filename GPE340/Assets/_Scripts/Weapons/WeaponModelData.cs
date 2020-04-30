﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Utility.Enums;

public class WeaponModelData : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("Weapon Materials"), Tooltip("The materials to use for each quality tier of the weapon.")]
    [SerializeField] private Material _baseMat;
    [SerializeField] private Material _uncommonMat;
    [SerializeField] private Material _rareMat;
    [SerializeField] private Material _epicMat;
    [SerializeField] private Material _legendaryMat;

    [Tooltip("The list of parts to apply materials to when the weapon quality changes. " +
             "Will be populated at runtime if the parts are not manually added."),
        Space, SerializeField] private List<MeshRenderer> _partMeshes = new List<MeshRenderer>();

    [Header("IK Positions"), 
        Tooltip("NOTE: These can be left null if they aren't needed for a particular weapon.")]
    [SerializeField] private Transform _lHandIKTransform;
    [SerializeField] private Transform _lElbowIKTransform;

    [Space, SerializeField] private Transform _rHandIKTransform;
    [SerializeField] private Transform _rElbowIKTransform;

    [Header("Weapon Components")]
    [Tooltip("The Transform component for the weapon's root GameObject (the Transform component" +
             "this script is residing on.)"),
        Space, SerializeField] private Transform _weaponTransform;
    [Tooltip("The transform component of the raycast origin object attached to the weapon model."),
        SerializeField] private Transform _raycastOriginTransform;
    [Tooltip("The muzzle flash particle effect attached to this weapon."),
        SerializeField] private ParticleSystem _muzzleFlash;
    [Tooltip("The IShootable component for this weapon."),
        SerializeField] private IShootable _weaponBehavior;
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

    /// <summary>
    /// The Transform component for the weapon's root GameObject (the Transform component this
    /// script is residing on.)
    /// </summary>
    public Transform WeaponTransform
    {
        get { return _weaponTransform; }
    }

    /// <summary>
    /// The transform component of the raycast origin object attached to the weapon model.
    /// </summary>
    public Transform RaycastOriginTransform
    {
        get { return _raycastOriginTransform; }
    }

    /// <summary>
    /// The IShootable component for this weapon.
    /// </summary>
    public IShootable WeaponBehavior
    {
        get { return _weaponBehavior; }
    }
    #endregion

    // Awake is called before Start
    protected virtual void Awake()
    {
        // Component reference assignments
        if (_weaponTransform == null)
        {
            _weaponTransform = this.gameObject.GetComponent<Transform>();
        }
        if (_weaponBehavior == null)
        {
            _weaponBehavior = this.gameObject.GetComponent<IShootable>();
        }

        // Stores the MeshRenderers of each part of the weapon so we can modify their materials
        _partMeshes = this.gameObject.GetComponentsInChildren<MeshRenderer>().ToList();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {

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
            Debug.Log($"{this.gameObject.name} does not have a material set for the chosen weapon quality!");
        }
#endif

        // Sets the material for each part of the weapon model
        foreach (MeshRenderer part in _partMeshes)
        {
            part.material = newMaterial;
        }
    }

    /// <summary>
    /// Plays this weapon's muzzle flash particle effect.
    /// </summary>
    public void PlayMuzzleFlash()
    {
        _muzzleFlash.Play();
    }
}