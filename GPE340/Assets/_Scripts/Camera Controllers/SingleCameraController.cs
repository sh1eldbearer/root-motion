using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCameraController : CameraController
{
    // Constants for number of zoom settings
    private const int MIN_OFFSET_HEIGHT_SETTING = 0;
    private const int MAX_OFFSET_HEIGHT_SETTING = 5;

    #region Private Properties
    [Tooltip("The move speed of the camera."),
        SerializeField] private float _followSpeed = 3.5f;
    [Tooltip("The interval distance the camera will zoom in or out from the follow target."),
        SerializeField, Range(0f, 2f)] private float _zoomFactor = 0.5f;
    [Tooltip("The camera's current zoom \"setting\" (how far in or out the camera is zoomed. (min = zoomed in, max = zoomed out)"),
        SerializeField, Range(MIN_OFFSET_HEIGHT_SETTING, MAX_OFFSET_HEIGHT_SETTING)] private int _zoomSetting = 3;
    private Vector3 _initialOffset; // The initial position of the camera relative to the object it is set to follow
    private Vector3 _heightOffset; // The "zoomed out" level of the camera relative to the object it is set to follow
    #endregion

    #region Game Components
#pragma warning disable 0649
    [Header("Game Components")]
    [Tooltip("The object this camera should follow."),
        SerializeField] private GameObject _followTarget;
    [SerializeField] private Transform _followTf; // The Transform component of the follow target object
    [SerializeField] private AgentController _followPawn; // The Pawn component of the follow target object
#pragma warning restore 0649
    #endregion

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        /* Component reference assignments */
        _followTf = _followTarget.transform;
        _followPawn = _followTarget.GetComponent<AgentController>();

        if (_followTarget != null)
        {
            // If that assigned Pawn is a player, assigns this camera as the pawn's camera
            if (_followPawn.GetType() == typeof(PlayerController))
            {
                ((PlayerController) _followPawn).AssignCamera(this.GetComponent<Camera>());
            }

            // Stores the camera's original position relative to its target
            _initialOffset = CameraTransform.position - _followTf.position;
            // Stores the camera's initial height offset
            SetHeightOffset();
            // Sets the camera's follow speed to match the move speed of its follow target
            _followSpeed = _followPawn.MoveSpeed;
        }
        else
        {
            // If I forgot to set a follow target for the camera, yells at me (but only in the editor)
#if UNITY_EDITOR
            Debug.LogError(string.Format("You forgot to set a follow target for {0}!", this.name));
#endif
        }

        StartCoroutine(AdjustCameraZoom());
    }

    /// <summary>
    /// Adjusts the current zoom setting of the camera
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
    /// Updates the camera position relative to the camera's follow target.
    /// </summary>
    /// <returns>Null.</returns>
    public override IEnumerator UpdateCameraPosition()
    {
        if (GameManager.gm.IsGameRunning && _followTarget != null)
        {
            // As long as the camera has a target to follow, updates the camera's position 
            CameraTransform.position = Vector3.MoveTowards(CameraTransform.position, _followTf.position + _initialOffset + _heightOffset,
                _followSpeed);
        }

        yield return null;
    }

    /// <summary>
    /// Changes the camera's zoom setting, allowing players to zoom their camera in or out to their preference.
    /// </summary>
    /// <param name="axisValue">The value of the input axis assigned to camera zoom.</param>
    /// <returns>Returns the current zoom setting value, or zero if the value was unchanged</returns>
    private int ChangeZoomSetting(float axisValue)
    {
        if (axisValue == 0f)
        {
            return 0;
        }

        if (axisValue > 0)
        {
            _zoomSetting =
                Mathf.Clamp(_zoomSetting + 1, MIN_OFFSET_HEIGHT_SETTING, MAX_OFFSET_HEIGHT_SETTING);
        }
        else if (axisValue < 0)
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

    /// <summary>
    /// Sets the camera's follow target.
    /// </summary>
    /// <param name="followTarget"></param>
    public void SetFollowTarget(GameObject followTarget)
    {
        _followTarget = followTarget;
    }
}
