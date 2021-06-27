using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTriggerAttack : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attack", false);
    }
}
