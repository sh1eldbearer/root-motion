using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    public float followSpeed = 3.5f;
    public Vector3 initalOffset;

    private Transform thisTf;
    private Transform followTf;
    private Pawn followPawn;

    // Start is called before the first frame update
    void Start()
    {
        /* Component reference assignments */
        thisTf = this.transform;
        followTf = followTarget.transform;
        followPawn = followTarget.GetComponent<Pawn>();

        if (followTarget != null)
        {
            // Stores the camera's original position relative to its target
            initalOffset = thisTf.position - followTf.position;
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.gm.gameIsRunning && followTarget != null)
        {
            // As long as the camera has a target to follow, updates the camera's position 
            thisTf.position = Vector3.MoveTowards(thisTf.position, followTf.position + initalOffset,
                followSpeed * Time.deltaTime);
        }
    }
}
