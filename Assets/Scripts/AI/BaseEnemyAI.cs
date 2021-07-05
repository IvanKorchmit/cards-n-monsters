using UnityEngine;

public class BaseEnemyAI : MonoBehaviour
{
    public PerkClass perk;
    public bool PerkStealingGuaranteed;
    protected virtual void Start()
    {
        PerkStealingGuaranteed = Random.Range(0, 100) >= 80;
        transform.Find("Sparkle").gameObject.SetActive(PerkStealingGuaranteed);
    }
    private void ShuffleInventory()
    {
        Item[] inv = GetComponent<Stats>().inventory;
        for (int i = 0; i < inv.Length; i++)
        {
            if(inv[i].item != null)
            {
                inv[i].quantity = Random.Range(0, inv[i].quantity);
                inv[i].item = inv[i].quantity > 0 ? inv[i].item : null;
            }

        }
    }
    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }
}
