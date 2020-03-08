using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandColliderAdjuster : StateMachineBehaviour
{
    [Tooltip("The height the collider should be when the agent is standing."), 
        SerializeField] private float _colliderStandHeight = 2f;
    [Tooltip("The speed at which the collider's size should adjust."),
        SerializeField] private float _speedAdjustRate = 5f;
    private CapsuleCollider _thisCollider;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Component reference assignments
        if (_thisCollider == null)
        {
            _thisCollider = animator.gameObject.GetComponent<CapsuleCollider>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Adjusts the height of the collider so that it becomes taller when the character stands up from crouching
        _thisCollider.height = Mathf.Lerp(_thisCollider.height, _colliderStandHeight, Time.deltaTime * _speedAdjustRate);
    }
}
