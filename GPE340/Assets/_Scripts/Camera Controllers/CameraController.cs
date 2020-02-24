using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraController : MonoBehaviour
{
    #region Private Properties
    [SerializeField] private Camera _thisCamera;
    [SerializeField] private Transform _cameraTransform; // The Transform component of this camera controller
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

    public virtual void Awake()
    {
        /* Component reference assignments */
        _thisCamera = this.gameObject.GetComponent<Camera>();
        _cameraTransform = this.transform;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    /// <summary>
    /// Updates the camera position.
    /// </summary>
    /// <returns>Null.</returns>
    public virtual IEnumerator UpdateCameraPosition()
    {
        yield return null;
    }

    /// <summary>
    /// Adjusts the current zoom setting of the camera.
    /// </summary>
    /// <returns>Null.</returns>
    public virtual IEnumerator AdjustCameraZoom()
    {
        yield return null;
    }
}
