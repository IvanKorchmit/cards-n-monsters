using UnityEngine;
using UnityEngine.UI;
using System.Linq;
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
        int initHealth = 10;
        string thirdDigit = filledValue.ToString();
        int res;
        int.TryParse(thirdDigit.Last().ToString(), out res);
        for (int i = 0; i < transform.childCount; i++)
        {
            Image heart = transform.GetChild(i).gameObject.GetComponent<Image>();
            //heart.SetNativeSize();
            //heart.GetComponent<RectTransform>().sizeDelta = new Vector2(heart.sprite.rect.width * canvas.scaleFactor, heart.sprite.rect.height * canvas.scaleFactor);
            if (filledValue >= initHealth)
            {
                heart.sprite = Full;
            }
            else if ((initHealth - filledValue) <= 5)
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
