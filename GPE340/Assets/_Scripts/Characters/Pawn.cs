using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    /* Public Properties */
    public float moveSpeed = 3.5f;
    public float turnSpeed = 60.0f;

    /* Private Properties */
    [Header("Component References")]
    [SerializeField, Tooltip("This object's Transform component")] protected Transform tf;
    [SerializeField, Tooltip("This object's Animator component")] protected Animator anim;

    // Start is called before the first frame update
    public virtual void Start()
    {
        /* Component reference assignemnts */
        tf = this.GetComponent<Transform>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual Vector3 Move()
    {
        // TODO: Replace with base functionality once AI controllers are set up
        return Vector3.negativeInfinity;
    }

    public virtual Vector3 HandleRotation(Camera pawnCamera = null)
    {
        // TODO: Replace with base functionality once AI controllers are set up
        return Vector3.negativeInfinity;
    }
}
