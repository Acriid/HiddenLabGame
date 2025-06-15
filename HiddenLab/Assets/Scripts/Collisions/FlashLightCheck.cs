using UnityEngine;

public class FlashLightCheck : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    public GameObject canvus;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!playerAttributes.HasFlashlight || !playerAttributes.KeyCard1)
            {
                canvus.SetActive(true);
            }
            else
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canvus.activeSelf)
            {
                canvus.SetActive(false);
            }
        }
    }
}
