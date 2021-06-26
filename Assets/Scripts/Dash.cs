using UnityEngine;
[CreateAssetMenu(fileName ="New Dash",menuName ="Perks/Dash")]
public class Dash : PerkClass
{
    public override void Use(GameObject owner)
    {
        if(owner.CompareTag("Enemy"))
        {
            Rigidbody2D rb = owner.GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position - (Vector2)GameObject.Find("Player").transform.position);
        }
    }
}