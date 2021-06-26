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
        float dist = Vector2.Distance(player.transform.position, transform.position);
        if (dist >= 4 && dist <= 10)
        {
            moveDirection = (player.transform.position - transform.position).normalized;
        }
        else
        {
            rb.velocity = (player.transform.position - transform.position).normalized * 3;  
        }
        if (rb.velocity.magnitude <= 0.5f)
        {
            rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
            animator.SetInteger("Speed", Mathf.RoundToInt(moveDirection.magnitude));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > 2)
        {
            if (collision.gameObject.TryGetComponent(out IDamagable dmg))
            {
                dmg.Damage(7, gameObject);
            }
            animator.ResetTrigger("Jump");
        }
    }
}