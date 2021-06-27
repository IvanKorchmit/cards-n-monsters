using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask layers;
    private int lyr;
    private Animator animator;
    private Camera main;
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        lyr = layers;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
            float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            int angle = Mathf.RoundToInt(ang / 90) * 90;
            angle = angle < 0 ? angle + 360 : angle;
            animator.SetInteger("Angle", angle);
            animator.SetBool("Attack", true);
        }
    }
    public void Attack()
    {
        Debug.Log("Attacking");
        Vector2 mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 origin = transform.position;
        Vector2 dir = (mousePos - (Vector2)transform.position).normalized;
        rb.velocity = dir * 5;


        var Ray = Physics2D.Raycast(origin + dir, dir, 1, lyr);
        Debug.DrawRay(origin + dir, dir);
        if (Ray.collider != null && Ray.collider.CompareTag("Enemy"))
        {
            Ray.collider?.GetComponent<IDamagable>().Damage(10, gameObject);
        }
    }
}
