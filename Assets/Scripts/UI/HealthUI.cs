using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    private Stats player;
    public Canvas canvas;
    [SerializeField] private Sprite Full;
    [SerializeField] private Sprite Half;
    [SerializeField] private Sprite Empty;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }

    private void OnGUI()
    {
        int filledValue = Mathf.RoundToInt((float) player.Health / player.MaxHealth * 100);
        int initHealth = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Image heart = transform.GetChild(i).gameObject.GetComponent<Image>();
            //heart.SetNativeSize();
            //heart.GetComponent<RectTransform>().sizeDelta = new Vector2(heart.sprite.rect.width * canvas.scaleFactor, heart.sprite.rect.height * canvas.scaleFactor);
            if (filledValue >= initHealth)
            {
                heart.sprite = Full;
            }
            else if (initHealth - filledValue <= 2)
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
