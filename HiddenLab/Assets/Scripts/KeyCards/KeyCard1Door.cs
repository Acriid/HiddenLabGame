using UnityEngine;

public class KeyCard1Door : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    private bool _keycard1;
    private BoxCollider2D[] boxcolliders;
    private BoxCollider2D nonTrigger;
    void OnEnable()
    {
        _keycard1 = playerAttributes.KeyCard1;
        playerAttributes.OnKeyCard1Change += HandleKeyCard1Change;
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
        playerAttributes.OnKeyCard1Change -= HandleKeyCard1Change;
    }

    void OnDestroy()
    {
        playerAttributes.OnKeyCard1Change -= HandleKeyCard1Change;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.CompareTag("Player") && _keycard1) || (collision.CompareTag("Enemy")))
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            nonTrigger.enabled = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if((collision.CompareTag("Player") && _keycard1) || (collision.CompareTag("Enemy")))
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            nonTrigger.enabled = true;
        }
    }
    void HandleKeyCard1Change(bool newValue)
    {
        _keycard1 = newValue;
    }
}
