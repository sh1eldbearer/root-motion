using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    /* Public Properties */
    public float moveSpeed = 3.5f;
    public float turnSpeed = 60.0f;
    public Camera myCamera;

    /* Private Properties */
    [Header("Component References (Can be set in inspector, but will be auto-assigned in code if left empty.")]
    [SerializeField, Tooltip("This object's Transform component")] private Transform tf;
    [SerializeField] private Animator anim; // The animator that controls this character

    // Start is called before the first frame update
    void Start()
    {
        /* Component reference assignemnts */
        tf = this.GetComponent<Transform>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the world vector that we want to move
        Vector3 worldMoveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Normalize it to allow controllers and keyboards to function the same
        worldMoveVector.Normalize();

        // Find local version of the worldMoveVector (relative to the object's transform)
        Vector3 localMoveVector = transform.InverseTransformDirection(worldMoveVector);

        // Pass values from the input controller into the animator to generate movement
        anim.SetFloat("Horizontal", localMoveVector.x * moveSpeed);
        anim.SetFloat("Vertical", localMoveVector.z * moveSpeed);

        HandleRotation();
    }

    void HandleRotation()
    {
        // Create a plane object for the plane the player is standing on
        Plane groundPlane = new Plane(Vector3.up, tf.position);

        // Create a ray from camera through the mouse position in the direction the camera is facing
        Ray mouseRay = myCamera.ScreenPointToRay(Input.mousePosition);

        // Raycast
        float intersectDistance;
        if (groundPlane.Raycast(mouseRay, out intersectDistance))
        {
            Vector3 collisionPoint = mouseRay.GetPoint(intersectDistance);

            // Get rotation needed to look at that point
            Quaternion targetRotation = Quaternion.LookRotation(collisionPoint - tf.position, tf.up);
            tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, turnSpeed * Time.deltaTime) ;
            //tf.LookAt(collisionPoint);
        }
        else
        {
            Debug.LogError("Camera is not looking at plane");
        }
    }
}
