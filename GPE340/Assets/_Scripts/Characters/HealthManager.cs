﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;

public class HealthManager : MonoBehaviour, IDamageable, IHealable
{
    #region Private Properties
#pragma warning disable CS0649
    [SerializeField] private UnityEvent _healthChanged;

    [Header("Game Components")]
    [Tooltip("The PawnData component for this Pawn."), 
        SerializeField] private PawnData _pawnData;
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
            _pawnData = this.gameObject.GetComponent<PawnData>();
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

    /// <summary>
    /// Adds a listener to the HealthChanged event.
    /// </summary>
    /// <param name="call">The name of the function to call when HealthChanged is invoked.</param>
    public void AddHealthChangedListener(UnityAction call)
    {
        // Adding a persistent listener allows me to visualize when events have been added
        #if UNITY_EDITOR
            UnityEventTools.AddPersistentListener(_healthChanged, call);
        #else
            _healthChanged.AddListener(call);
        #endif
    }

    /// <summary>
    /// Adds multiple listeners to the HealthChanged event.
    /// </summary>
    /// <param name="calls">The names of the functions to call when HealthChanged is invoked.</param>
    public void AddHealthChangedListeners(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            AddHealthChangedListener(call);
        }
    }

    /// <summary>
    /// Removes a listener from the HealthChanged event.
    /// </summary>
    /// <param name="call">The name of the function to remove from the HealthChanged invoke array.</param>
    public void RemoveHealthChangedListener(UnityAction call)
    {
        // Removing a persistent listener allows me to visualize when events have been removed
        #if UNITY_EDITOR
            UnityEventTools.RemovePersistentListener(_healthChanged, call);
        #else
            _healthChanged.AddListener(call);
        #endif
    }

    /// <summary>
    /// Removes multiple listeners from the HealthChanged event.
    /// </summary>
    /// <param name="calls">The names of the functions to remove from the HealthChanged invoke array.</param>
    public void RemoveHealthChangedListeners(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            RemoveHealthChangedListener(call);
        }
    }

    /// <summary>
    /// Takes a specified amount of damage, and alerts all listeners that the health of this pawn has changed.
    /// </summary>
    /// <typeparam name="T">The data type of the damage value (should match PawnData's CurrentHealth and MaxHealth.)</typeparam>
    /// <param name="incomingDmg">The amount of damage this pawn will take.</param>
    public void TakeDamage<T> (T dmgAmount)
    {
        // TODO: Damage functionality (mostly just reduce current health and a death check)

        // Notifies all listeners that this pawn's health has changed
        _healthChanged.Invoke();
    }

    /// <summary>
    /// Receives a specified amount of healing, and alerts all listeners that the health of this pawn has changed.
    /// </summary>
    /// <typeparam name="T">The data type of the damage value (should match PawnData's CurrentHealth and MaxHealth.)</typeparam>
    /// <param name="healAmount">The amount of damage this pawn will take.</param>
    public void ReceiveHealing<T>(T healAmount)
    {
        // TODO: Heal functionality (mostly just increase current health)

        // Notifies all listeners that this pawn's health has changed
        _healthChanged.Invoke();
    }
}
