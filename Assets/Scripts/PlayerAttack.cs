using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 origin = transform.position;
            Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
            rb.velocity = dir * 5;
            var Ray = Physics2D.Raycast(origin + dir, dir, 5);
            Debug.DrawRay(origin + dir, dir);
            if (Ray.collider != null && Ray.collider.CompareTag("Enemy"))
            {
                Ray.collider?.GetComponent<IDamagable>().Damage(10);
            }
        }
    }
}
