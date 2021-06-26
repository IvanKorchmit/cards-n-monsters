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

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }
}