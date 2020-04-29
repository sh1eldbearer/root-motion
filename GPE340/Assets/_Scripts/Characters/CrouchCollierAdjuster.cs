using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class CrouchCollierAdjuster : StateMachineBehaviour
{
    #region Private Properties
    [Tooltip("This agent's data component."),
        SerializeField] private PawnData _pawnData;
    #endregion

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Component reference assignments
        if (_pawnData == null)
        {
            _pawnData = animator.gameObject.GetComponent<PawnData>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Adjusts the height of the collider so that it becomes shorter when the character crouches down from standing
        _pawnData.PawnCollider.height = Mathf.Lerp(_pawnData.PawnCollider.height, _pawnData.CrouchColliderHeight, 
            Time.deltaTime * _pawnData.ColliderAdjustSpeed);
        _pawnData.PawnCollider.center = Vector3.Lerp(_pawnData.PawnCollider.center, _pawnData.CrouchColliderCenter,
            Time.deltaTime * _pawnData.ColliderAdjustSpeed);
    }
}