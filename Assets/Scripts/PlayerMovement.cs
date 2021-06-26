using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 rawMovement; // Raw Movement
    [SerializeField] private float speed;
    private Vector2 lastMove;
    private Rigidbody2D rb;
    private Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        float ang = Mathf.Atan2(lastMove.y, lastMove.x) * Mathf.Rad2Deg;
        int angle = Mathf.RoundToInt(ang / 90) * 90;
        angle = angle < 0 ? angle + 360 : angle;
        if (rb.velocity.magnitude <= 0.5f)
        {
            rb.MovePosition(rb.position + rawMovement * speed * Time.deltaTime);
            animator.SetInteger("Angle", angle);
        }
    }
    private void Update()
    {
        rawMovement.x = Input.GetAxisRaw("Horizontal");
        rawMovement.y = Input.GetAxisRaw("Vertical");
        if(rawMovement != Vector2.zero)
        {
            lastMove = rawMovement;
        }
    }
}
