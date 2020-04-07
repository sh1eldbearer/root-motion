using System.Collections;
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

    [Header("IK Positions"), 
        Tooltip("NOTE: These can be left null if they aren't needed for a particular weapon.")]
    [SerializeField] private Transform _lHandIKTransform;
    [SerializeField] private Transform _lElbowIKTransform;
    [SerializeField] private Transform _rHandIKTransform;
    [SerializeField] private Transform _rElbowIKTransform;

    [Header("IK Settings")]
    [Tooltip("The weight to apply to the position of the avatar's left hand for inverse kinematics."),
     SerializeField, Range(0.0f, 1.0f)] private float _lHandIKPositionWeight = 1.0f;
    [Tooltip("The weight to apply to the rotation of the avatar's left hand for inverse kinematics."),
        SerializeField, Range(0.0f, 1.0f)] private float _lHandIKRotationWeight = 1.0f;
    [Tooltip("The weight to apply to the position of the avatar's left elbow for inverse kinematics."),
        SerializeField, Range(0.0f, 1.0f)] private float _lElbowIKPositionWeight = 1.0f;
    [Tooltip("The weight to apply to the rotation of the avatar's left elbow for inverse kinematics."),
        SerializeField, Range(0.0f, 1.0f)] private float _lElbowIKRotationWeight = 1.0f;

    [Tooltip("The weight to apply to the position of the avatar's right hand for inverse kinematics."),
        Space, SerializeField, Range(0.0f, 1.0f)] private float _rHandIKPositionWeight = 1.0f;
    [Tooltip("The weight to apply to the rotation of the avatar's right hand for inverse kinematics."),
        SerializeField, Range(0.0f, 1.0f)] private float _rHandIKRotationWeight = 1.0f;
    [Tooltip("The weight to apply to the position of the avatar's right elbow for inverse kinematics."),
        SerializeField, Range(0.0f, 1.0f)] private float _rElbowIKPositionWeight = 1.0f;
    [Tooltip("The weight to apply to the rotation of the avatar's right elbow for inverse kinematics."),
        SerializeField, Range(0.0f, 1.0f)] private float _rElbowIKRotationWeight = 1.0f;
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
    /// The weight to apply to the position of the avatar's left hand for inverse kinematics.
    /// </summary>
    public float LHandIKPositionWeight
    {
        get { return _lHandIKPositionWeight; }
    }

    /// <summary>
    /// The weight to apply to the rotation of the avatar's left hand for inverse kinematics.
    /// </summary>
    public float LHandIKRotationWeight
    {
        get { return _lHandIKRotationWeight; }
    }

    /// <summary>
    /// The weight to apply to the position of the avatar's left elbow for inverse kinematics.
    /// </summary>
    public float LElbowIKPositionWeight
    {
        get { return _lElbowIKPositionWeight; }
    }

    /// <summary>
    /// The weight to apply to the rotation of the avatar's left elbow for inverse kinematics.
    /// </summary>
    public float LElbowIKRotationWeight
    {
        get { return _lElbowIKRotationWeight; }
    }

    /// <summary>
    /// The weight to apply to the position of the avatar's right hand for inverse kinematics.
    /// </summary>
    public float RHandIKPositionWeight
    {
        get { return _rHandIKPositionWeight; }
    }

    /// <summary>
    /// The weight to apply to the rotation of the avatar's right hand for inverse kinematics.
    /// </summary>
    public float RHandIKRotationWeight
    {
        get { return _rHandIKRotationWeight; }
    }

    /// <summary>
    /// The weight to apply to the position of the avatar's right elbow for inverse kinematics.
    /// </summary>
    public float RElbowIKPositionWeight
    {
        get { return _rElbowIKPositionWeight; }
    }

    /// <summary>
    /// The weight to apply to the rotation of the avatar's right elbow for inverse kinematics.
    /// </summary>
    public float RElbowIKRotationWeight
    {
        get { return _rElbowIKRotationWeight; }
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
