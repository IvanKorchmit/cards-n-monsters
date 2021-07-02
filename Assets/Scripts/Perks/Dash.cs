using UnityEngine;
[CreateAssetMenu(fileName ="New Dash",menuName ="Perks/Dash")]
public class Dash : PerkClass
{
    public override void Use(GameObject owner)
    {
        Rigidbody2D rb = owner.GetComponent<Rigidbody2D>();
        if(owner.CompareTag("Enemy"))
        {
            Vector2 player = GameObject.FindGameObjectWithTag("Player").transform.position;
            rb.velocity = (player - rb.position).normalized * 20;
        }
        else
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.velocity = (mousePos - rb.position).normalized * 20;
            float ang = Mathf.Atan2(rb.velocity.normalized.y, rb.velocity.normalized.x) * Mathf.Rad2Deg;
            int angle = Mathf.RoundToInt(ang / 90) * 90;
            angle = angle < 0 ? angle + 360 : angle;
            owner.GetComponent<Animator>().SetInteger("Angle", angle);
        }
    }
}