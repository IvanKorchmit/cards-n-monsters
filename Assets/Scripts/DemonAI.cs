using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAI : BaseEnemyAI
{
    public PerkClass[] perks;
    private Vector2 moveDirection;
    private Stats stats;
    public float speed;
    private Rigidbody2D rb;
    GameObject player;
    private Animator animator;
    public int stage;
    public int currentPerk;
    public float Cooldown;
    private float curTime;
    private bool enraged;
    protected override void Start()
    {
        stats = GetComponent<Stats>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public void UsePerk()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 20)
        {
            perks[currentPerk].Use(gameObject);
            currentPerk = (currentPerk + 1) % perks.Length;
        }
    }
    public void Enrage()
    {
        if (!enraged)
        {
            Cooldown /= 2;
            speed *= 1.5f;
            enraged = true;
        }
    }
    protected override void FixedUpdate()
    {
        curTime += Time.deltaTime;
        if(curTime >= Cooldown)
        {
            curTime = 0;
            UsePerk();
        }
        if(stats.Health <= stats.MaxHealth / 2)
        {
            Enrage();
        }
        Move();
    }

    private void Move()
    {
        float ang = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        int angle = Mathf.RoundToInt(ang / 90) * 90;
        angle = angle < 0 ? angle + 360 : angle;

        animator.SetInteger("Angle", angle);
        if (player != null)
        {
            float dist = Vector2.Distance(player.transform.position, transform.position);
            if (dist >= 4 && dist <= 10)
            {
                moveDirection = (player.transform.position - transform.position).normalized;
            }
            else if (dist < 4)
            {
                rb.velocity = (player.transform.position - transform.position).normalized * 2;
            }
            else
            {
                moveDirection = Vector2.zero;
            }
            if (rb.velocity.magnitude <= 0.5f)
            {
                rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
                animator.SetInteger("Speed", Mathf.RoundToInt(moveDirection.magnitude));
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(40, gameObject, 20);
        }
    }
}
