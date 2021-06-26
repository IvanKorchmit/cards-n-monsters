using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    private Stats player;
    private Image image;
    [SerializeField] private Sprite Full;
    [SerializeField] private Sprite Half;
    [SerializeField] private Sprite Empty;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        image = GetComponent<Image>();
    }

    private void OnGUI()
    {
        int filledValue = Mathf.RoundToInt(player.Health / player.MaxHealth * 100);
        int initHealth = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Image heart = transform.GetChild(i).gameObject.GetComponent<Image>();
            if (filledValue >= initHealth)
            {
                heart.sprite = Full;
            }
            else if (initHealth - filledValue <= 5)
            {
                heart.sprite = Half;
            }
            else
            {
                heart.sprite = Empty;
            }
            initHealth += 10;
        }
    }
}
