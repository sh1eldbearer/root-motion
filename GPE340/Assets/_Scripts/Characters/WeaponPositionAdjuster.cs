using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class WeaponPositionAdjuster : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Header("Game Components")]
    [Tooltip("The Transform component of the GameObject the weapon container will use to determine its y-position."),
        SerializeField] private Transform _weaponAnchorTransform;
    [Tooltip("The Transform component for this object."), 
        SerializeField] private Transform _thisTransform;
#pragma warning restore CS0649
    #endregion
	
	// Awake is called before Start
	private void Awake()
	{
		// Component reference assignments
        if (_thisTransform == null)
        {
            _thisTransform = this.transform;
        }

        // Register this function's coroutines with the pause manager events
        PauseManager.pauseMgr.AddListeners(StartPositionCoroutine, StopPositionCoroutine);
    }

    private void OnEnable()
    {
        StartPositionCoroutine();
    }

    private void OnDisable()
    {
        StopPositionCoroutine();
    }

    /// <summary>
    /// Starts the coroutine responsible for maintaining this object's y-position relative to the
    /// object it's using as an anchor point.
    /// </summary>
    private void StartPositionCoroutine()
    {
        StartCoroutine(MaintainRelativePosition());
    }

    /// <summary>
    /// Stops the coroutine responsible for maintaining this object's y-position relative to the
    /// object it's using as an anchor point.
    /// </summary>
    private void StopPositionCoroutine()
    {
        StopCoroutine(MaintainRelativePosition());
    }

    /// <summary>
    /// Maintains this object's y-position relative to the object it's using as an anchor point.
    /// </summary>
    private IEnumerator MaintainRelativePosition()
    {
        while (true)
        {
            _thisTransform.position =
                new Vector3(_thisTransform.position.x, _weaponAnchorTransform.position.y, _thisTransform.position.z);

            yield return null;
        }
    }
}
