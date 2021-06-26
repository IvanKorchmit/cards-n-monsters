using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeAI : BaseEnemyAI
{
    private Vector2 moveDirection;
    public float speed;
    private Rigidbody2D rb;
    private GameObject player;
    [SerializeField] private float Cooldown;
    private Animator animator;
    private float time;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    protected override void FixedUpdate()
    {
        float ang = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        int angle = Mathf.RoundToInt(ang / 90) * 90;
        angle = angle < 0 ? angle + 360 : angle;

        animator.SetInteger("Angle",angle);

        time += Time.deltaTime;
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if (dist >= 4 && dist <= 10)
        {
            moveDirection = player.transform.position - transform.position;
            if (time >= Cooldown)
            {
                perk.Use(gameObject);
                time = 0;
            }
        }
        else
        {
            moveDirection = (player.transform.position + transform.position) / 4;
        }
        if (rb.velocity.magnitude <= 0.5f)
        {
            rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
            animator.SetInteger("Speed", Mathf.RoundToInt(moveDirection.magnitude));
        }
    }
}
