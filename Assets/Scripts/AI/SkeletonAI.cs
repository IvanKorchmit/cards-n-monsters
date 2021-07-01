using UnityEngine;

public class SkeletonAI : BaseEnemyAI
{
    public float speed;
    private Animator animator;
    private Rigidbody2D rb;
    public string TeamTag;
    private Vector2 moveDirection;
    private GameObject player;
    private bool isRaising = true;
    private GameObject closest;
    private new void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator.Play("Raise");
        animator.speed = Random.Range(0.7f, 1.5f);
    }
    private new void FixedUpdate()
    {
        if(isRaising)
        {
            return;
        }
        if(TeamTag == "Player")
        {
            closest = Utils.FindClosest("Enemy", gameObject);
        }
        float ang = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        int angle = Mathf.RoundToInt(ang / 90) * 90;
        angle = angle < 0 ? angle + 360 : angle;

        animator.SetInteger("Angle", angle);
        float dist = TeamTag != "Player" && closest != null && player != null ? Vector2.Distance(closest.transform.position, transform.position) : Vector2.Distance(player.transform.position, transform.position);
        if (dist >= 4 && dist <= 10)
        {
            if (TeamTag != "Player")
            {
                moveDirection = (player.transform.position - transform.position).normalized;
            }
            else if (closest != null)
            {
                moveDirection = (closest.transform.position - transform.position).normalized;
            }
            else
            {
                moveDirection = Vector2.zero;
            }
        }
        else
        {


            if (TeamTag != "Player")
            {
                rb.velocity = (player.transform.position - transform.position).normalized * 9;
            }
            else if (closest != null)
            {
                rb.velocity = (closest.transform.position - transform.position).normalized * 9;
            }
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
            dmg.Damage(1, gameObject,8);
            if (TeamTag != "Player")
            {
                rb.velocity = -(player.transform.position - transform.position).normalized * 9;
            }
            else if (closest != null)
            {
                rb.velocity = -(closest.transform.position - transform.position).normalized * 9;

            }
        }
    }
    public void FinishRaising()
    {
        isRaising = false;
    }
}


public static class Utils
{
    public static GameObject FindClosest(string tag, GameObject origin)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        GameObject result = null;
        GameObject lastObject = null;
        if (objects != null && objects.Length > 0)
        {
            float distance = Vector2.Distance(objects[0].transform.position, origin.transform.position);
            int iteration = 0;
            foreach (GameObject item in objects)
            {
                if (distance >= Vector2.Distance(item.transform.position, origin.transform.position))
                {
                    lastObject = item;
                    distance = Vector2.Distance(item.transform.position, origin.transform.position);
                }
                iteration++;
            }
            result = lastObject;
        }
        return result;
    }
}