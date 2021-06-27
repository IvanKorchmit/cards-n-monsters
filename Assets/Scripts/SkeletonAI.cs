using UnityEngine;

public class SkeletonAI : BaseEnemyAI
{
    public float speed;
    private Animator animator;
    private Rigidbody2D rb;
    public string TeamTag;
    private Vector2 moveDirection;
    private GameObject player;
    private new void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private new void FixedUpdate()
    {
        float ang = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        int angle = Mathf.RoundToInt(ang / 90) * 90;
        angle = angle < 0 ? angle + 360 : angle;

        animator.SetInteger("Angle", angle);


        float dist = Vector2.Distance(player.transform.position, transform.position);
        if (dist >= 4 && dist <= 10)
        {
            rb.velocity = (player.transform.position - transform.position).normalized * 9;  
        }
        else
        {
            moveDirection = (player.transform.position - transform.position).normalized;
        }
        if (rb.velocity.magnitude <= 0.5f)
        {
            rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
            animator.SetInteger("Speed", Mathf.RoundToInt(moveDirection.magnitude));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagable dmg))
        {
            dmg.Damage(7, gameObject);
            rb.velocity = -(player.transform.position - transform.position).normalized * 9;
        }
    }
}