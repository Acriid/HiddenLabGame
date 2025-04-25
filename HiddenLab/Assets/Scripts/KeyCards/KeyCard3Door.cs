using UnityEngine;

public class KeyCard3Door : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    private bool _keycard3;
    private BoxCollider2D[] boxcolliders;
    private BoxCollider2D nonTrigger;
    void OnEnable()
    {
        _keycard3 = playerAttributes.KeyCard3;
        playerAttributes.OnKeyCard3Change += HandleKeyCard3Change;
        boxcolliders = this.GetComponents<BoxCollider2D>();
        foreach(BoxCollider2D box in boxcolliders)
        {
            if(box.isTrigger == false)
            {
                nonTrigger = box;
                break;
            }
        }
    }
    void OnDisable()
    {
        playerAttributes.OnKeyCard3Change -= HandleKeyCard3Change;
    }

    void OnDestroy()
    {
        playerAttributes.OnKeyCard3Change -= HandleKeyCard3Change;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.CompareTag("Player") && _keycard3) || (collision.CompareTag("Enemy")))
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            nonTrigger.enabled = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if((collision.CompareTag("Player") && _keycard3) || (collision.CompareTag("Enemy")))
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            nonTrigger.enabled = true;
        }
    }
    void HandleKeyCard3Change(bool newValue)
    {
        _keycard3 = newValue;
    }
}
