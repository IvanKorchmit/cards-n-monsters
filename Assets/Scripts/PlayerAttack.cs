using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask layers;
    private int lyr;
    private Animator animator;
    private Stats stats;
    private Camera main;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Stats>();
        main = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        lyr = layers;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
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
    }
    public void Attack()
    {
        float distance = 1;
        int damage = 1;
        Vector2 mousePos, origin, dir;
        mousePos = main.ScreenToWorldPoint(Input.mousePosition);
        origin = transform.position;
        dir = (mousePos - (Vector2)transform.position).normalized;
        rb.velocity = dir * 5;
        if (stats.weapon is Sword sword)
        {
            distance = sword.distance;
            damage = sword.damage;
            transform.Find("Sword").GetComponent<SpriteRenderer>().sprite = sword?.sprite ?? null;
        }
        var Ray = Physics2D.CircleCast(origin + dir, 1.5f, dir, distance, lyr);
        Debug.DrawRay(origin + dir, dir);
        if (Ray.collider != null && Ray.collider.CompareTag("Enemy"))
        {
            Ray.collider?.GetComponent<IDamagable>().Damage(damage, gameObject, 8);
        }

    }
}
