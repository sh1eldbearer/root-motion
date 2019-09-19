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
        return Vector3.zero;
    }

    public virtual Vector3 HandleRotation(Camera pawnCamera = null)
    {
        // Create a plane object for the plane the player is standing on
        Plane groundPlane = new Plane(Vector3.up, tf.position);
        
        // Create a ray from camera through the mouse position in the direction the camera is facing
        if (pawnCamera != null)
        {
            // Uses the mouse position to rotate the pawn
            Ray mouseRay = pawnCamera.ScreenPointToRay(Input.mousePosition);

            // Raycast
            float intersectDistance;
            if (groundPlane.Raycast(mouseRay, out intersectDistance))
            {
                // TODO: Dead zone when mouse is under player's feet
                Vector3 collisionPoint = mouseRay.GetPoint(intersectDistance);

                // Get rotation needed to look at that point
                //Quaternion targetRotation = Quaternion.LookRotation(collisionPoint - tf.position, tf.up);
                //tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, turnSpeed * Time.deltaTime) ;
                Vector3 lookVector = (collisionPoint - tf.position).normalized; 
                tf.LookAt(collisionPoint + lookVector);
                return collisionPoint;
            }
            else
            {
                Debug.LogError("Camera is not looking at plane");
                return Vector3.negativeInfinity;
            }
        }
        else
        {
            // TODO: For AI movement (not yet implemented)
            return Vector3.negativeInfinity;
        }
    }
}
