using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchCollierAdjuster : StateMachineBehaviour
{
    [SerializeField] private float colliderCrouchHeight = 1f;
    [SerializeField] private float speedAdjustRate = 5f;
    private CapsuleCollider thisCollider;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thisCollider = animator.gameObject.GetComponent<CapsuleCollider>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        thisCollider.height = Mathf.Lerp(thisCollider.height, colliderCrouchHeight, Time.deltaTime * speedAdjustRate);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}
}
