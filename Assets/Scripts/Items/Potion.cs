using UnityEngine;

public class Potion : BaseItem
{
    public override void Use(GameObject owner, ref bool consumed)
    {
        consumed = true;
    }
}
