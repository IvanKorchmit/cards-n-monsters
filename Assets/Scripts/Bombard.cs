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
            bomb.GetComponent<Bomb>().Init(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }
}
