using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;

public class HealthManager : MonoBehaviour, IDamageable, IHealable, IKillable
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("Health Settings")]
    [Tooltip("The current health value of this pawn."),
        SerializeField] private float _currentHealth = 100;
    [Tooltip("The maximum health value of this pawn."),
        SerializeField] private float _maxHealth = 100;

    [Tooltip("An event that notifies listeners when this pawn's current health has changed."),
        Space, SerializeField] private UnityEvent _currentHealthChanged;
    [Tooltip("An event that notifies listeners when this pawn's max health has changed."),
        SerializeField] private UnityEvent _maxHealthChanged;
    [Tooltip("An event that notifies listeners when this pawn has been killed."),
        SerializeField] private UnityEvent _onKilled;

    [Header("Game Components")]
    [Tooltip("The PawnData component for this Pawn."), 
        SerializeField] private PawnData _pawnData;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The current health value of this pawn.
    /// </summary>
    public float CurrentHealth
    {
        get { return _currentHealth; }
    }

    /// <summary>
    /// The maximum health value of this pawn.
    /// </summary>
    public float MaxHealth
    {
        get { return _maxHealth; }
    }
    #endregion

    // Awake is called before Start
    private void Awake()
    {
        // Component reference assignments
        if (_pawnData == null)
        {
            _pawnData = this.gameObject.GetComponentInParent<PawnData>();
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

    public void InitializeHealthValues(float initialValue)
    {
        _maxHealth = initialValue;
        _maxHealthChanged.Invoke();
        _currentHealth = initialValue;
        _currentHealthChanged.Invoke();
    }

    /// <summary>
    /// Adds one or more listeners to the CurrentHealthChanged event.
    /// </summary>
    /// <param name="calls">The names of the functions to call when CurrentHealthChanged is invoked.</param>
    public void AddCurrentHealthChangedListener(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            // Adding a persistent listener allows me to visualize when events have been added
            #if UNITY_EDITOR
                UnityEventTools.AddPersistentListener(_currentHealthChanged, call);
            #else
                _currentHealthChanged.AddListener(call);
            #endif
        }
    }

    /// <summary>
    /// Removes one or more listeners from the CurrentHealthChanged event.
    /// </summary>
    /// <param name="calls">The names of the functions to remove from the CurrentHealthChanged invoke array.</param>
    public void RemoveCurrentHealthChangedListener(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            // Removing a persistent listener allows me to visualize when events have been removed
            #if UNITY_EDITOR
                UnityEventTools.RemovePersistentListener(_currentHealthChanged, call);
            #else
                _currentHealthChanged.AddListener(call);
            #endif
        }
    }

    /// <summary>
    /// Adds one or more listeners to the MaxHealthChanged event.
    /// </summary>
    /// <param name="calls">The names of the functions to call when MaxHealthChanged is invoked.</param>
    public void AddMaxHealthChangedListener(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            // Adding a persistent listener allows me to visualize when events have been added
            #if UNITY_EDITOR
                UnityEventTools.AddPersistentListener(_maxHealthChanged, call);
            #else
                _maxHealthChanged.AddListener(call);
            #endif
        }
    }

    /// <summary>
    /// Removes one or more listeners from the MaxHealthChanged event.
    /// </summary>
    /// <param name="calls">The names of the functions to remove from the MaxHealthChanged invoke array.</param>
    public void RemoveMaxHealthChangedListener(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            // Removing a persistent listener allows me to visualize when events have been removed
            #if UNITY_EDITOR
                UnityEventTools.RemovePersistentListener(_maxHealthChanged, call);
            #else
                _maxHealthChanged.AddListener(call);
            #endif
        }
    }

    /// <summary>
    /// Adds one or more listeners to the OnKilled event.
    /// </summary>
    /// <param name="calls">The names of the functions to call when OnKilled is invoked.</param>
    public void AddOnKilledListener(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            // Adding a persistent listener allows me to visualize when events have been added
            #if UNITY_EDITOR
                UnityEventTools.AddPersistentListener(_onKilled, call);
            #else
                _currentHealthChanged.AddListener(call);
            #endif
        }
    }

    /// <summary>
    /// Removes one or more listeners from the OnKilled event.
    /// </summary>
    /// <param name="calls">The names of the functions to remove from the OnKilled invoke array.</param>
    public void RemoveOnKilledListener(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            // Removing a persistent listener allows me to visualize when events have been removed
            #if UNITY_EDITOR
                UnityEventTools.RemovePersistentListener(_onKilled, call);
            #else
                _currentHealthChanged.AddListener(call);
            #endif
        }
    }

    /// <summary>
    /// Takes a specified amount of damage, and alerts all listeners that the health of this pawn has changed.
    /// </summary>
    /// <typeparam name="T">The data type of the damage value (should match type of CurrentHealth and MaxHealth.)</typeparam>
    /// <param name="incomingDmg">The amount of damage this pawn will take.</param>
    public void TakeDamage(float dmgAmount)
    {
        // TODO: Damage functionality (mostly just reduce current health and a death check)

        // Subtracts the damage amount from the pawn's current health (won't let the health go below zero)
        _currentHealth = Mathf.Clamp(_currentHealth - dmgAmount, 0f, _maxHealth);

        // If the pawn has zero health, kill it
        if (_currentHealth <= 0f)
        {
            KillMe();
        }

        // Notifies all listeners that this pawn's health has changed
        _currentHealthChanged.Invoke();
    }

    /// <summary>
    /// Receives a specified amount of healing, and alerts all listeners that the health of this pawn has changed.
    /// </summary>
    /// <typeparam name="T">The data type of the damage value (should match type of CurrentHealth and MaxHealth.)</typeparam>
    /// <param name="healAmount">The amount of damage this pawn will take.</param>
    public void ReceiveHealing(float healAmount)
    {
        // TODO: Heal functionality (mostly just increase current health)

        // Adds the heal amount to the pawn's current health
        _currentHealth = Mathf.Clamp(_currentHealth + healAmount, 0f, _maxHealth);

        // Notifies all listeners that this pawn's health has changed
        _currentHealthChanged.Invoke();
    }

    /// <summary>
    /// Kills this pawn when its health reaches zero.
    /// </summary>
    public void KillMe()
    {
        // TODO: Death functionality
        _pawnData.Controller.StopAllCoroutines();
        _pawnData.PawnAnimator.SetTrigger("Dead");
        _pawnData.InventoryMgr.EquippedWeaponModelData.DropWeapon();


        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.gm.PlayerRespawnTimer);

        _pawnData.Controller.StartAllCoroutines();
        _pawnData.PawnAnimator.SetTrigger("Dead");
        _pawnData.InventoryMgr.EquippedWeaponModelData.ResetWeaponPosition();
        _pawnData.HealthMgr.InitializeHealthValues(GameManager.gm.InitialPlayerHealth);
    }
}
