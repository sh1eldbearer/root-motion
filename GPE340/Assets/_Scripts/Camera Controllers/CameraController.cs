﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraController : MonoBehaviour
{
    #region Private Properties
    [Header("Camera Components")]
    [Tooltip("This camera's Camera component."),
        SerializeField] private Camera _thisCamera;
    [Tooltip("This camera's Transform component."),
        SerializeField] private Transform _cameraTransform;
    #endregion

    #region Public Properties
    /// <summary>
    /// The Camera component of this camera controller.
    /// </summary>
    public Camera ThisCamera
    {
        get { return _thisCamera; }
        protected set { _thisCamera = value; }
    }
    /// <summary>
    /// The Transform component of this camera controller.
    /// </summary>
    public Transform CameraTransform
    {
        get { return _cameraTransform; }
        protected set { _cameraTransform = value; }
    }

    #endregion

    // Awake is called before Start
    public virtual void Awake()
    {
        // Component reference assignments
        _thisCamera = _thisCamera ?? this.gameObject.GetComponent<Camera>();
        _cameraTransform = _cameraTransform ?? this.transform;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    /// <summary>
    /// Updates the camera position.
    /// </summary>
    public virtual void UpdateCameraPosition()
    {

    }

    /// <summary>
    /// Adjusts the current zoom setting of the camera.
    /// </summary>
    /// <returns>Null.</returns>
    protected virtual IEnumerator AdjustCameraZoom()
    {
        yield return null;
    }
}
