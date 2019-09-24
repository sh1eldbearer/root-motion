using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    /* Public Properties */
    [Tooltip("The movement speed of this pawn")] public float moveSpeed = 3.5f;
    public float turnSpeed = 60f;

    [Space, Tooltip("The camera that will be following this pawn's movements")]public Camera pawnCamera;

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

    public virtual Vector3 HandleRotation()
    {
        // TODO: Replace with base functionality once AI controllers are set up
        return Vector3.negativeInfinity;
    }

    public virtual Vector3 HandleRotation(Camera pawnCamera)
    {
        // TODO: Replace with base functionality once AI controllers are set up
        return Vector3.negativeInfinity;
    }

    /// <summary>
    /// Sets the sprinting and walking booleans in this pawn's animator to the provided values.
    /// Both parameters cannot be true simultaneously.
    /// </summary>
    /// <param name="isSprinting">Whether or not the pawn should be currently sprinting. Cannot
    /// be true while isCrouching is also true.</param>
    /// <param name="isCrouching">Whether or not the pawn should be currently crouching. Cannot
    /// be true while isSprinting is also true.</param>
    protected void SetRunWalkBools(bool isSprinting, bool isCrouching)
    {
        // Throws an error if both arguments are true
        if (isSprinting && isCrouching)
        {
            throw new ArgumentException(string.Format(
                "The Pawn {0} cannot be both sprinting and walking at the same time. Please change one of the arguments to false.",
                this.name));
        }

        // Sets the animator parameters
        anim.SetBool("isSprinting", isSprinting);
        anim.SetBool("isCrouching", isCrouching);
    }
}
