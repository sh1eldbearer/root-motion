using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [HideInInspector] public Animator anim; // The animator that controls this character

    // Start is called before the first frame update
    void Start()
    {
        /* Component reference assignemnts */
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Pass values from the input controller into the animator to generate movement
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
    }
}
