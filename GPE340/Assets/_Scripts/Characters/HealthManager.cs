using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;

public class HealthManager : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [SerializeField] private UnityEvent _healthChanged;
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
            _onPause.AddListener(call);
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
            _onPause.AddListener(call);
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
}
