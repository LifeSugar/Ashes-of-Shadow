using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNormalizedTime3 : StateMachineBehaviour
{
    private string targetParameter = "Normalized Time3";
    private string isAttacking = "isAttacking";

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.SetBool(isAttacking, true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(targetParameter, stateInfo.normalizedTime);
        animator.SetFloat(targetParameter, 0);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(targetParameter, 0);
        animator.SetBool(isAttacking, false);
    }
}
