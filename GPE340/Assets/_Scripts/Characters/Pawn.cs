using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Pass the values on the "controller" from the input manager into the animator
        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal") );
        anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
    }
}
