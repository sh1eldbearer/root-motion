using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingleCameraController : CameraController
{

    /* Public Properties */

    /* Game Components */
#pragma warning disable 0649
    [Tooltip("The object this camera should follow."), 
     SerializeField] private GameObject followTarget;
#pragma warning restore 0649

    /* Private Properties */
    [Tooltip("The move speed of the camera."),
     SerializeField] private float followSpeed = 3.5f;
    [Tooltip("The interval distance the camera will zoom in or out from the follow target."),
     SerializeField, Range(0f, 2f)] private float zoomFactor = 0.5f;
    private Vector3 initalOffset; // The initial position of the camera relative to the object it is set to follow
    private Vector3 heightOffset; // The "zoomed out" level of the camera relative to the object it is set to follow
    private Transform cameraTf; // The Transform component of this camera object
    private Transform followTf; // The Transform component of the follow target object
    private AgentController followPawn; // The Pawn component of the follow target object
    [Range(MIN_OFFSET_HEIGHT_SETTING, MAX_OFFSET_HEIGHT_SETTING)] private int zoomSetting = 3;

    // Constants for number of zoom settings
    private const int MIN_OFFSET_HEIGHT_SETTING = 0;
    private const int MAX_OFFSET_HEIGHT_SETTING = 5;

    // Start is called before the first frame update
    public override void Start()
    {
        /* Component reference assignments */
        cameraTf = this.transform;
        followTf = followTarget.transform;
        followPawn = followTarget.GetComponent<AgentController>();

        if (followTarget != null)
        {
            // If that assigned Pawn is a player, assigns this camera as the pawn's camera
            if (followPawn.GetType() == typeof(PlayerController))
            {
                ((PlayerController) followPawn).pawnCamera = this.GetComponent<Camera>();
            }

            // Stores the camera's original position relative to its target
            initalOffset = cameraTf.position - followTf.position;
            // Stores the camera's initial height offset
            SetHeightOffset();
            // Sets the camera's follow speed to match the move speed of its follow target
            followSpeed = followPawn.moveSpeed;
        }
        else
        {
            // If I forgot to set a follow target for the camera, yells at me (but only in the editor)
#if UNITY_EDITOR
            Debug.LogError(string.Format("You forgot to set a follow target for {0}!", this.name));
#endif
        }
    }

    public override void FixedUpdate()
    {
        if (GameManager.gm.IsGameRunning && followTarget != null)
        {
            // As long as the camera has a target to follow, updates the camera's position 
            cameraTf.position = Vector3.MoveTowards(cameraTf.position, followTf.position + initalOffset + heightOffset,
                followSpeed);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        // Changes the camera's zoom setting (must be in Update; FixedUpdate can skip over input changes causing unresponsiveness
        ChangeZoomSetting(Input.GetAxis("Mouse ScrollWheel"));
    }

    /// <summary>
    /// Changes the camera's zoom setting, allowing players to zoom their camera in or out to their preference.
    /// </summary>
    /// <param name="axisValue">The value of the input axis assigned to camera zoom.</param>
    /// <returns></returns>
    private int ChangeZoomSetting(float axisValue)
    {
        if (axisValue != 0)
        {
            if (axisValue > 0)
            {
                zoomSetting =
                    Mathf.Clamp(zoomSetting + 1, MIN_OFFSET_HEIGHT_SETTING, MAX_OFFSET_HEIGHT_SETTING);
            }
            else if (axisValue < 0)
            {
                zoomSetting =
                    Mathf.Clamp(zoomSetting - 1, MIN_OFFSET_HEIGHT_SETTING, MAX_OFFSET_HEIGHT_SETTING);
            }

            // Sets the new target camera height
            SetHeightOffset();
        }
        else
        {
            // If there is no axis value, don't change the zoom setting
        }

        return zoomSetting;
    }

    /// <summary>
    /// Sets the value of the camera's "zoom" adjustment
    /// </summary>
    private void SetHeightOffset()
    {
        heightOffset = new Vector3(0f, this.zoomSetting * this.zoomFactor, 0f);
    }
}
