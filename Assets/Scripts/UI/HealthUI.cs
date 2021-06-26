using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    private Stats player;
    private Image image;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        image = GetComponent<Image>();
    }

    private void OnGUI()
    {
        float filledValue = player.Health / player.MaxHealth;
        image.fillAmount = filledValue;
    }
}
