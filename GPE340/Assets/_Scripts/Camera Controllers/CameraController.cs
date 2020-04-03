using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Constants
#pragma warning disable CS0649
    // Constants for number of zoom settings
    private const int MIN_OFFSET_HEIGHT_SETTING = 0;
    private const int MAX_OFFSET_HEIGHT_SETTING = 5;
    #endregion

    #region Private Properties
    [Header("Movement Settings")]
    [Tooltip("The move speed of the camera."),
        SerializeField] private float _followSpeed = 3.5f;

    [Header("Zoom Settings")]
    [Tooltip("The interval distance the camera will zoom in or out from the follow target."),
     SerializeField, Range(0f, 2f)] private float _zoomFactor = 0.5f;
    [Tooltip("The camera's current zoom \"setting\" (how far in or out the camera is zoomed. (min = zoomed in, max = zoomed out)"),
     SerializeField, Range(MIN_OFFSET_HEIGHT_SETTING, MAX_OFFSET_HEIGHT_SETTING)] private int _zoomSetting = 3;
    private Vector3 _initialOffset; // The initial position of the camera relative to the object it is set to follow
    private Vector3 _heightOffset; // The "zoomed out" level of the camera relative to the object it is set to follow

    [Header("Camera Initial Positions/Rotations")]
    [Tooltip("The worldspace position this camera should snap to at the start of the game " +
             "if the camera doesn't have a target it's following."),
        SerializeField] private Vector3 _neutralPosition;
    [Tooltip(""),
        SerializeField] private Vector3 _neutralRotation;
    [Tooltip("The worldspace position this camera should snap to at the start of the game " +
             "if the camera has a target it's following."), Space(1.5f),
        SerializeField] private Vector3 _followPosition;
    [Tooltip(""),
        SerializeField] private Vector3 _followRotation;

    [Header("Camera Components")]
    [Tooltip("This camera's Camera component."),
        SerializeField] private Camera _thisCamera;
    [Tooltip("This camera's Transform component."),
        SerializeField] private Transform _cameraTransform;

    [Header("Object to Follow Components")]
    [Tooltip("The object this camera should follow."),
        SerializeField] private GameObject _followTarget;
    [Tooltip("The Transform component of the GameObject being followed by this camera."), 
        SerializeField] private Transform _followTf;
    [Tooltip("The Pawn component of the GameObject being followed by this camera."),
        SerializeField] private PawnData _followData;
#pragma warning restore CS0649
    #endregion

    #region Public Properties
    /// <summary>
    /// The Transform component of the GameObject being followed by this camera.
    /// </summary>
    public Transform FollowTf
    {
        get { return _followTf; }
    }

    #endregion

    // Awake is called before Start
    public void Awake()
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

        if (_followTarget != null)
        {
            SetFollowTarget();
        }
        else
        {
            ClearFollowTarget();
        }

        GameManager.gm.SetGameCamera(_thisCamera, this);
    }

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(AdjustCameraZoom());
    }

    /// <summary>
    /// Updates the camera position.
    /// </summary>
    public void UpdateCameraPosition()
    {
        if (GameManager.gm.IsGameRunning && _followTarget != null)
        {
            // As long as the camera has a target to follow, updates the camera's position 
            _cameraTransform.position = Vector3.MoveTowards(_cameraTransform.position, _followTf.position + _initialOffset + _heightOffset,
                _followSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Adjusts the current zoom setting of the camera.
    /// </summary>
    /// <returns>Null.</returns>
    private IEnumerator AdjustCameraZoom()
    {
        while (true)
        {
            if (GameManager.gm.IsGameRunning)
            {
                ChangeZoomSetting(Input.GetAxis("Mouse ScrollWheel"));
            }

            yield return null;
        }
    }

    /// <summary>
    /// Changes the camera's zoom setting, allowing players to zoom their camera in or out to their preference.
    /// </summary>
    /// <param name="axisValue">The value of the input axis assigned to camera zoom.</param>
    /// <returns>Returns the current zoom setting value, or zero if the value was unchanged.</returns>
    private int ChangeZoomSetting(float axisValue)
    {
        if (axisValue == 0f)
        {
            return 0;
        }

        if (axisValue < 0) // Zoom out
        {
            _zoomSetting =
                Mathf.Clamp(_zoomSetting + 1, MIN_OFFSET_HEIGHT_SETTING, MAX_OFFSET_HEIGHT_SETTING);
        }
        else if (axisValue > 0) // Zoom in
        {
            _zoomSetting =
                Mathf.Clamp(_zoomSetting - 1, MIN_OFFSET_HEIGHT_SETTING, MAX_OFFSET_HEIGHT_SETTING);

        }

        SetHeightOffset();

        return _zoomSetting;
    }

    /// <summary>
    /// Sets the value of the camera's "zoom" adjustment
    /// </summary>
    private void SetHeightOffset()
    {
        _heightOffset = new Vector3(0f, this._zoomSetting * this._zoomFactor, 0f);
    }

    private void SetPositionAndRotation(Vector3 position, Vector3 rotation)
    {
        _cameraTransform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
    }

    /// <summary>
    /// Sets the camera's follow target.
    /// </summary>
    private void SetFollowTarget()
    {
        _followData = _followTarget.GetComponentInChildren<PawnData>();
        _followTf = _followData.PawnTransform;

        SetPositionAndRotation(_followPosition, _followRotation);

        // If the assigned agent is a player, assigns this camera as the pawn's camera
        if (_followData.Controller.GetType() == typeof(PlayerController))
        {
            _followData.AssignCameraController(this);
        }

        // Sets the camera's follow speed to match the move speed of its follow target
        _followSpeed = _followData.MoveSpeed;
        // Stores the camera's original position relative to its target
        _initialOffset = _cameraTransform.position - _followTf.position;
        // Stores the camera's initial height offset
        SetHeightOffset();
    }

    /// <summary>
    /// Sets the camera's follow target.
    /// </summary>
    /// <param name="followTarget"></param>
    public void SetFollowTarget(GameObject followTarget)
    {
        _followTarget = followTarget;
        SetFollowTarget();
    }

    public void ClearFollowTarget()
    {
        _followTarget = null;
        _followTf = null;
        _followData = null;
        SetPositionAndRotation(_neutralPosition, _neutralRotation);
    }
}
