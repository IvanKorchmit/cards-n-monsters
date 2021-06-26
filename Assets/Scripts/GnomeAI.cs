using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeAI : BaseEnemyAI
{
    private Vector2 moveDirection;
    public float speed;
    private Rigidbody2D rb;
    private GameObject player;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    protected override void FixedUpdate()
    {
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if (dist >= 4 && dist <= 10)
        {
            moveDirection = player.transform.position - transform.position;
        }
        else
        {
            moveDirection = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f));
        }
        if (rb.velocity.magnitude <= 0.5f)
        {
            rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
        }
    }
}
