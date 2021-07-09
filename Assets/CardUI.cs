using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardUI : MonoBehaviour
{
    PlayerPerks player;
    Image img;
    GameObject hint;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPerks>();
        hint = transform.Find("Hint").gameObject;
        img = GetComponent<Image>();
    }
    private void OnGUI()
    {
        if (player.perk != null)
        {
            img.sprite = player?.perk.icon ?? null;
            img.color = Color.white;
            hint.SetActive(true);
        }
        else
        {
            img.sprite = null;
            img.color = Color.clear;
            hint.SetActive(false);
        }
    }
}
