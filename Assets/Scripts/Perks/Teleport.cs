using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "New Teleport", menuName = "Perks/Teleport")]
public class Teleport : PerkClass
{
    public int damage;
    public override void Use(GameObject owner)
    {
        if(owner.CompareTag("Enemy"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                owner.transform.position = player.transform.position;
                player.GetComponent<IDamagable>().Damage(Random.Range(1, damage), owner, 8);
            }
        }
        else
        {
            var ray = Physics2D.CircleCastAll(owner.transform.position, 5, Vector2.zero);
            List<RaycastHit2D> enemies = new List<RaycastHit2D>();
            for (int i = 0; i < ray.Length; i++)
            {
                if(ray[i].collider != null && ray[i].collider.CompareTag("Enemy"))
                {
                    enemies.Add(ray[i]);
                }
            }
            if (enemies.Count == 0) return; 
            var enemy = enemies[Random.Range(0, enemies.Count)].collider.gameObject;
            owner.transform.position = enemy.transform.position;
            enemy.GetComponent<IDamagable>().Damage(damage * 2, owner, 16);
        }
    }
}