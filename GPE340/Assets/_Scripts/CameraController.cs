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

    // Start is called before the first frame update
    void Start()
    {
        thisTf = this.transform;
        followTf = followTarget.transform;
        initalOffset = thisTf.position - followTf.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (followTarget != null)
        {
            thisTf.position = Vector3.MoveTowards(thisTf.position, followTf.position + initalOffset, followSpeed * Time.deltaTime);
        }
    }
}
