using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNormalizedTime2 : StateMachineBehaviour
{
    private string targetParameter = "Normalized Time2";
    private string isattacking = "isAttacking";

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(targetParameter, 0);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(targetParameter, stateInfo.normalizedTime);
        animator.SetBool(isattacking, true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(targetParameter, 0);
        animator.SetBool(isattacking, false);
    }
}
