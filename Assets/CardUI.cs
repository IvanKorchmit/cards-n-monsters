using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardUI : MonoBehaviour
{
    PlayerPerks player;
    Image img;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPerks>();
        img = GetComponent<Image>();
    }
    private void OnGUI()
    {
        img.sprite = player.perk.icon;
    }
}
