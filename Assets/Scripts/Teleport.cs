using UnityEngine;
[CreateAssetMenu(fileName = "New Teleport", menuName = "Perks/Teleport")]
public class Teleport : PerkClass
{
    public int damage;
    public override void Use(GameObject owner)
    {
        if(owner.CompareTag("Enemy"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            owner.transform.position = player.transform.position;
            player.GetComponent<IDamagable>().Damage(Random.Range(1, damage), owner);
        }
    }
}