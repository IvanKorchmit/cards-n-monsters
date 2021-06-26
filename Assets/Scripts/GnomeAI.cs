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
    private float time;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    protected override void FixedUpdate()
    {
        time += Time.deltaTime;
        if(time >= Cooldown)
        {
            perk.Use(gameObject);
        }
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if (dist >= 4 && dist <= 10)
        {
            moveDirection = player.transform.position - transform.position;
        }
        else
        {
            moveDirection = (player.transform.position + transform.position) / 4;
        }
        if (rb.velocity.magnitude <= 0.5f)
        {
            rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
        }
    }
}
