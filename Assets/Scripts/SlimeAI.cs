using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    public PerkClass perk;
    private GameObject player;
    public bool IsDashing;
    private Animator animator;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        InvokeRepeating("TriggerAnimation", 0, Random.Range(1f, 2));
    }
    public void Jump()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= 10)
        {
            perk.Use(gameObject);
        }
    }
    private void TriggerAnimation()
    {
        animator.SetTrigger("Jump");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GetComponent<Rigidbody2D>().velocity.magnitude > 2)
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(5);
            animator.ResetTrigger("Jump");
        }
    }
}
