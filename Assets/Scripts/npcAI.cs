using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcAI : MonoBehaviour
{
    private Vector2 moveDirection;
    public float speed;
    private Rigidbody2D rb;
    private Animator animator;
    public float Cooldown;
    private float timer;
    public bool isBusy;
    private Transform hint;
    // Start is called before the first frame update
    void Start()
    {
        hint = transform.Find("Hint");
        hint.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if(timer >= Cooldown)
        {
            timer = 0;
            moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            StartCoroutine(Move());
        }
        
    }
    public Stats Interact()
    {
        return GetComponent<Stats>();
    }
    private IEnumerator Move()
    {
        float ang = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        int angle = Mathf.RoundToInt(ang / 90) * 90;
        angle = angle < 0 ? angle + 360 : angle;

        animator.SetInteger("Angle", angle);
        for (int i = 0; i < 1000 * Time.deltaTime; i++)
        {
            if (rb.velocity.magnitude <= 0.5f && !isBusy)
            {
                rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
                animator.SetInteger("Speed", Mathf.RoundToInt(moveDirection.magnitude));
            }
            yield return null;
        }
        animator.SetInteger("Speed", 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            hint.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Test");
            hint.gameObject.SetActive(false);
        }
    }
}
