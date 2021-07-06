using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UILevel : MonoBehaviour
{
    private Image xp;
    private TextMeshProUGUI level;
    private TextMeshProUGUI xpText;
    private void Start()
    {
        xp = transform.Find("Filled").GetComponent<Image>();
        level = transform.Find("Counter").GetComponent<TextMeshProUGUI>();
        xpText = transform.Find("XP").GetComponent<TextMeshProUGUI>();
    }
    private void OnGUI()
    {
        float value = (float)PlayerLevel.xp / PlayerLevel.maxXP;
        xp.fillAmount = value;
        level.text = $"{PlayerLevel.Level}";
        xpText.text = $"{PlayerLevel.xp}/{PlayerLevel.maxXP}";
    }
}
