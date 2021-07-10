using UnityEngine;

[CreateAssetMenu(fileName = "Healing Potion", menuName = "Items/Potions/Healing")]
public class HealingPotion : Potion
{
    public int healingStrength;
    public override void Use(GameObject owner, ref bool consumed)
    {
        base.Use(owner, ref consumed);
        owner.GetComponent<Stats>().Heal(healingStrength);
        SoundsStatic.PlayPitched(SoundsStatic.Drink);
    }
}