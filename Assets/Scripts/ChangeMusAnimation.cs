using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ChangeMusAnimation : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private AudioSource au;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        au = Camera.main.GetComponent<AudioSource>();
        au.volume = 0;
        animator.ResetTrigger("Change");
        au.clip = MusicZoneTheme.newMusic;
        au.Play();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        au.volume += Time.deltaTime / (stateInfo.length * Settings.musicVolume);
        au.volume = Mathf.Clamp(au.volume, 0, Settings.musicVolume);
    }
}
