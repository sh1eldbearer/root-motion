using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;

public class PauseManager : MonoBehaviour
{
    public static PauseManager pauseMgr; // Singleton instance for the PauseManager

    #region Private Properties
#pragma warning disable CS0649
    [SerializeField] private UnityEvent _onPause = new UnityEvent();
    [SerializeField] private UnityEvent _onUnpause = new UnityEvent();
#pragma warning restore CS0649
    #endregion

    // Awake is called before Start
    private void Awake()
    {
        // Makes the GameManager a singleton and a persistent game object
        if (pauseMgr == null)
        {
            pauseMgr = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Adds a listener to the OnPause event.
    /// </summary>
    /// <param name="call">The name of the function to call when OnPause is invoked.</param>
    public void AddOnPauseListener(UnityAction call)
    {
        // Adding a persistent listener allows me to visualize when events have been added
        #if UNITY_EDITOR
            UnityEventTools.AddPersistentListener(_onPause, call);
        #else
            _onPause.AddListener(call);
        #endif
    }

    /// <summary>
    /// Adds multiple listeners to the OnPause event.
    /// </summary>
    /// <param name="calls">The names of the functions to add from the OnPause invoke array.</param>
    public void AddOnPauseListeners(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            AddOnPauseListener(call);
        }
    }

    /// <summary>
    /// Removes a listener from the OnPause event.
    /// </summary>
    /// <param name="call">The name of the function to remove from the OnPause invoke array.</param>
    public void RemoveOnPauseListener(UnityAction call)
    {
        // Removing a persistent listener allows me to visualize when events have been removed
        #if UNITY_EDITOR
            UnityEventTools.RemovePersistentListener(_onPause, call);
        #else
            _onUnpause.AddListener(call);
        #endif
    }

    /// <summary>
    /// Removes multiple listeners from the OnPause event.
    /// </summary>
    /// <param name="calls">The names of the functions to remove from the OnPause invoke array.</param>
    public void RemoveOnPauseListeners(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            RemoveOnPauseListener(call);
        }
    }

    /// <summary>
    /// Adds a listener to the OnUnpause event.
    /// </summary>
    /// <param name="call">The name of the function to call when OnUnpause is invoked.</param>
    public void AddOnUnpauseListener(UnityAction call)
    {
        // Adding a persistent listener allows me to visualize when events have been added
        #if UNITY_EDITOR
            UnityEventTools.AddPersistentListener(_onUnpause, call);
        #else
            _onUnpause.AddListener(call);
        #endif
    }

    /// <summary>
    /// Adds multiple listeners to the OnUnpause event.
    /// </summary>
    /// <param name="calls">The names of the functions to call when OnUnpause is invoked.</param>
    public void AddOnUnpauseListeners(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            AddOnUnpauseListener(call);
        }
    }

    /// <summary>
    /// Removes a listener from the OnUnpause event.
    /// </summary>
    /// <param name="call">The name of the function to remove from the OnUnpause invoke array.</param>
    public void RemoveOnUnpauseListener(UnityAction call)
    {
        // Removing a persistent listener allows me to visualize when events have been removed
        #if UNITY_EDITOR
            UnityEventTools.AddPersistentListener(_onUnpause, call);
        #else
            _onUnpause.RemoveListener(call);
        #endif
    }

    /// <summary>
    /// Removes multiple listeners from the OnUnpause event.
    /// </summary>
    /// <param name="calls">The names of the functions to remove from the OnUnpause invoke array.</param>
    public void RemoveOnUnpauseListeners(params UnityAction[] calls)
    {
        foreach (UnityAction call in calls)
        {
            RemoveOnUnpauseListener(call);
        }
    }

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
        _onPause.Invoke();
    }

    /// <summary>
    /// Unpauses the game.
    /// </summary>
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        _onUnpause.Invoke();
    }
}