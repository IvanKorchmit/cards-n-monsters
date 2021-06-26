using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 rawMovement; // Raw Movement
    private Vector2 moveDirection; // Processed movement direction
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 vel = new Vector2();
        moveDirection = Vector2.SmoothDamp(moveDirection, rawMovement, ref vel, Time.smoothDeltaTime, speed, Time.deltaTime);
        Vector2 direction = moveDirection * speed;
        rb.MovePosition(rb.position + direction);
    }
    private void Update()
    {
        rawMovement.x = Input.GetAxisRaw("Horizontal");
        rawMovement.y = Input.GetAxisRaw("Vertical");
    }
}
