using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusAnimation : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Change");
        Camera.main.GetComponent<AudioSource>().clip = MusicZoneTheme.newMusic;
        Camera.main.GetComponent<AudioSource>().Play();
    }
}
