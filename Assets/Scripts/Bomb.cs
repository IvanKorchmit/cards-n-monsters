using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;
    private Vector2 destination;
    public void Init(Vector2 Target)
    {
        Animator animator = GetComponent<Animator>();
        float dist = Vector2.Distance(Target, transform.position);
        animator.speed = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / dist * 10;
        destination = Target;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, 0.2f);
        if((Vector2)transform.position == destination)
        {
            Explode();
        }
    }
    private void Explode()
    {
        var expl = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(expl, expl.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length);
        var hit = Physics2D.CircleCastAll(transform.position, 3, Vector2.zero, 0.01f);
        foreach (RaycastHit2D item in hit)
        {
            if(item.collider != null && item.collider.gameObject.TryGetComponent(out IDamagable damage))
            {
                damage.Damage(10, gameObject, 20);
            }
        }
        Destroy(gameObject);
    }
}
