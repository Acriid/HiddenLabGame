using UnityEngine;

public class KeyCard2Door : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    private bool _keycard2;
    private BoxCollider2D[] boxcolliders;
    private BoxCollider2D nonTrigger;
    void OnEnable()
    {
        _keycard2 = playerAttributes.KeyCard2;
        playerAttributes.OnKeyCard2Change += HandleKeyCard2Change;
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
        playerAttributes.OnKeyCard2Change -= HandleKeyCard2Change;
    }

    void OnDestroy()
    {
        playerAttributes.OnKeyCard2Change -= HandleKeyCard2Change;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.CompareTag("Player") && _keycard2) || (collision.CompareTag("Enemy")))
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            nonTrigger.enabled = false;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if((collision.CompareTag("Player") && _keycard2) || (collision.CompareTag("Enemy")))
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            nonTrigger.enabled = true;
        }
    }
    void HandleKeyCard2Change(bool newValue)
    {
        _keycard2 = newValue;
    }
}
