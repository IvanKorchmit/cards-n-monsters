using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTriggerDamage : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Reset trigger");
        animator.ResetTrigger("Damage");
    }

}
