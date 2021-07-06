using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseVolume : StateMachineBehaviour
{
    AudioSource audio;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio = animator.GetComponent<AudioSource>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audio.volume -= Time.deltaTime;
    }
}
