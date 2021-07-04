using UnityEngine;

[CreateAssetMenu(fileName = "New Bombard", menuName = "Perks/Bombard")]
public class Bombard : PerkClass
{
    public GameObject bomb;
    public override void Use(GameObject owner)
    {
        var bomb = Instantiate(this.bomb, owner.transform.position, Quaternion.identity);
        if (owner.CompareTag("Player"))
        {
            bomb.GetComponent<Bomb>().Init(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                bomb.GetComponent<Bomb>().Init(player.transform.position);
            }
        }
    }
}
