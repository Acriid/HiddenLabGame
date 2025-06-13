using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentKeyCard : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    [SerializeField] private Sprite[] sprites;
    private Image image;
    void OnEnable()
    {
        image = this.GetComponent<Image>();
        if (playerAttributes.KeyCard1)
        {
            image.sprite = sprites[0];
        }
        else if (playerAttributes.KeyCard2)
        {
            image.sprite = sprites[1];
        }
        else if (playerAttributes.KeyCard3)
        {
            image.sprite = sprites[2];
        }   
    }
    void OnDisable()
    {
        
    }
}
