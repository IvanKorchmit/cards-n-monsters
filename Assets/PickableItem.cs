using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private Rigidbody2D rb;
    public Item item;
    private IEnumerator Start()
    {
        transform.Find("visual").GetComponent<SpriteRenderer>().sprite = item.item?.sprite ?? null;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 10;
        yield return new WaitForSeconds(0.5f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Stats>().AddItem(item))
            {
                Destroy(gameObject);
            }
        }
    }
}
