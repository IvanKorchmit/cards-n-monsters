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
            Debug.Log("Trying to jump to player");
            rb.velocity = (player - rb.position).normalized * 20;
        }
        else
        {
            Debug.Log("Else");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.velocity = (mousePos - rb.position).normalized * 20;
        }
    }
}