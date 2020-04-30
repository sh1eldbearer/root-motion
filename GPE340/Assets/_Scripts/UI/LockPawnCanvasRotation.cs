using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPawnCanvasRotation : StateMachineBehaviour
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
            _pawnData = animator.gameObject.GetComponentInParent<PawnData>();
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion

        // Locks this canvas's rotation to the designated rotation
        _pawnData.PawnCanvasTransform.rotation = _pawnData.PawnCanvasRotation;
    }
}
