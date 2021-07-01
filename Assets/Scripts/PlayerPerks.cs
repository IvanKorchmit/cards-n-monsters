using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerks : MonoBehaviour
{
    public PerkClass perk;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(perk != null)
            {
                perk.Use(gameObject);
            }
        }
    }
}
