using UnityEngine;

[CreateAssetMenu(fileName = "New Necronomicon", menuName = "Perks/Necronomicon")]
public class Necronomicon : PerkClass
{
    public GameObject Skeleton;
    public int playerLayer;
    public override void Use(GameObject owner)
    {
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0) continue;
                int layer = playerLayer;
                var Skeleton = Instantiate(this.Skeleton, owner.transform.position + new Vector3(x,y,0), Quaternion.identity);
                Skeleton.GetComponent<SkeletonAI>().TeamTag = owner.tag;
                Skeleton.tag = owner.tag;
                if (owner.CompareTag("Player"))
                {
                    Skeleton.layer = layer;
                }
            }
        }
    }
}