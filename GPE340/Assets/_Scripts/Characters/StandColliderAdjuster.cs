using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandColliderAdjuster : StateMachineBehaviour
{
    [Tooltip("This agent's data component."),
        SerializeField] private PawnData _pawnData;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Component reference assignments
        _pawnData = _pawnData ?? animator.gameObject.GetComponent<PawnData>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Adjusts the height of the collider so that it becomes taller when the character stands up from crouching
        _pawnData.PawnCollider.height = Mathf.Lerp(_pawnData.PawnCollider.height, _pawnData.StandColliderHeight, 
            Time.deltaTime * _pawnData.ColliderAdjustSpeed);
        _pawnData.PawnCollider.center = Vector3.Lerp(_pawnData.PawnCollider.center, _pawnData.StandColliderCenter,
            Time.deltaTime * _pawnData.ColliderAdjustSpeed);
    }
}
