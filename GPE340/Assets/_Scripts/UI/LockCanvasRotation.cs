using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCanvasRotation : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip("The target euler angles this canvas will attempt to lock itself to."),
        SerializeField] private Vector3 _targetRotation;
#pragma warning restore CS0649
    #endregion
	
	// Awake is called before Start
	private void Awake()
    {
        _targetRotation = this.transform.rotation.eulerAngles;
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    private void OnEnable()
    {
        // Register coroutines with the pause manager
        PauseManager.pauseMgr.AddOnUnpauseListener(StartLockRotationCoroutine);
        PauseManager.pauseMgr.AddOnPauseListener(StopLockRotationCoroutine);
    }

    private void OnDisable()
    {
        // Unregister coroutines with the pause manager
        PauseManager.pauseMgr.RemoveOnUnpauseListener(StartLockRotationCoroutine);
        PauseManager.pauseMgr.RemoveOnPauseListener(StopLockRotationCoroutine);
    }

    public void StartLockRotationCoroutine()
    {
        StartCoroutine(LockRotation());
    }

    public void StopLockRotationCoroutine()
    {
        StopCoroutine(LockRotation());
    }

    // Update is called once per frame
    private IEnumerator LockRotation()
    {
        while (true)
        {
            this.transform.rotation = Quaternion.Euler(_targetRotation);
            yield return null;
        }
    }


}
