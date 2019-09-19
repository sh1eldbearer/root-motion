using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController :  Pawn
{
    [Space]
    public Camera pawnCamera;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        Move();
        HandleRotation(pawnCamera);

        base.Update();
    }

    public override Vector3 Move()
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

        return localMoveVector;
    }
}
