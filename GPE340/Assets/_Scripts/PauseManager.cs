using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        _onPause.AddListener(call);
    }

    /// <summary>
    /// Removes a listener from the OnPause event.
    /// </summary>
    /// <param name="call">The name of the function to remove from the OnPause invoke array.</param>
    public void RemoveOnPauseListener(UnityAction call)
    {
        _onPause.RemoveListener(call);
    }

    /// <summary>
    /// Adds a listener to the OnUnpause event.
    /// </summary>
    /// <param name="call">The name of the function to call when OnUnpause is invoked.</param>
    public void AddOnUnpauseListener(UnityAction call)
    {
        _onUnpause.AddListener(call);
    }

    /// <summary>
    /// Removes a listener from the OnUnpause event.
    /// </summary>
    /// <param name="call">The name of the function to remove from the OnUnpause invoke array.</param>
    public void RemoveOnUnpauseListener(UnityAction call)
    {
        _onUnpause.RemoveListener(call);
    }

    /// <summary> 
    /// Adds a pair of listeners to the OnPause and OnUnpause events.
    /// </summary>
    /// <param name="pauseCall">The name of the function to call when OnPause is invoked.</param>
    /// <param name="unpauseCall">The name of the function to call when OnUnpause is invoked.</param>
    public void AddListeners(UnityAction pauseCall, UnityAction unpauseCall)
    {
        AddOnPauseListener(pauseCall);
        AddOnUnpauseListener(unpauseCall);
    }

    /// <summary>
    /// Removes a pair of listeners from the OnPause and OnUnpause events.
    /// </summary>
    /// <param name="pauseCall">The name of the function to remove from the OnPause invoke array.</param>
    /// <param name="unpauseCall">The name of the function to remove from the OnUnpause invoke array.</param>
    public void RemoveListeners(UnityAction pauseCall, UnityAction unpauseCall)
    {
        RemoveOnPauseListener(pauseCall);
        RemoveOnUnpauseListener(unpauseCall);
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