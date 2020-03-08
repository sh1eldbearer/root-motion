using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandColliderAdjuster : StateMachineBehaviour
{
    [Tooltip("This agent's data component."),
        SerializeField] private AgentData _agentData;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Component reference assignments
        if (_agentData == null)
        {
            _agentData = animator.gameObject.GetComponent<AgentData>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Adjusts the height of the collider so that it becomes taller when the character stands up from crouching
        _agentData.AgentCollider.height = Mathf.Lerp(_agentData.AgentCollider.height, _agentData.StandColliderHeight, 
            Time.deltaTime * _agentData.ColliderAdjustSpeed);
        _agentData.AgentCollider.center = Vector3.Lerp(_agentData.AgentCollider.center, _agentData.StandColliderCenter,
            Time.deltaTime * _agentData.ColliderAdjustSpeed);
    }
}
