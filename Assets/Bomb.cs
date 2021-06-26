using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Vector2 destination;
    public void Init(Vector2 Target)
    {
        Animator animator = GetComponent<Animator>();
        float dist = Vector2.Distance(Target, transform.position);
        animator.speed = dist / animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        destination = Target;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, 0.2f);
    }
}
