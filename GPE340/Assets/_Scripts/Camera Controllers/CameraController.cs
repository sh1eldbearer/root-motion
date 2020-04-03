using System.Collections;
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
    }
    /// <summary>
    /// The Transform component of this camera controller.
    /// </summary>
    public Transform CameraTransform
    {
        get { return _cameraTransform; }
    }

    #endregion

    // Awake is called before Start
    public virtual void Awake()
    {
        // Component reference assignments

        /* The null-coalescing operator (??) doesn't work with these two references, so I'm
           using if statements to perform the same function. (I thought it might be an issue 
           with inheritance, but testing them on references local to the child class also 
           fails to properly assign for some reason. I think this an issue with Unity's 
           backend but I don't have the time to dive too deeply down the rabbit hole to see 
           if it's ever properly been fixed or addressed.
           
           Here's a brief thread on this issue: 
           https://issuetracker.unity3d.com/issues/null-coalescing-operator-does-not-work-for-the-transform-component*/
        if (_thisCamera == null)
        {
            _thisCamera = this.gameObject.GetComponent<Camera>();
        }
        if (_cameraTransform == null)
        {
            _cameraTransform = this.transform;
        }
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
