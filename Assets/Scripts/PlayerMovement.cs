using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 rawMovement; // Raw Movement
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (rb.velocity.magnitude <= 0.5f)
        {
            rb.MovePosition(rb.position + rawMovement * speed * Time.deltaTime);
        }
    }
    private void Update()
    {
        rawMovement.x = Input.GetAxisRaw("Horizontal");
        rawMovement.y = Input.GetAxisRaw("Vertical");

    }
}
