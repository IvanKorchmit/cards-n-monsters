using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFixer : MonoBehaviour
{
    public Animator animator;
    private SpriteRenderer sprite;
    private void Start()
    {
        animator = GetComponent<Animator>();
        sprite = transform.Find("visuals").GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        bool currAnimationDamage = animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Damage")).IsName("Damage");
        if(!currAnimationDamage)
        {
            sprite.color = Color.white;
        }
    }
}
