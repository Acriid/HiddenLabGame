using UnityEngine;

public class Doors : MonoBehaviour
{
    private Doorsound doorSound;
    private BoxCollider2D doorCollider;
    private Animator animator;
    public PlayerAttributes playerAttributes;
    private bool _KeyCard1;
    void Start()
    {
        // Get the Doorsound component from the same GameObject
        doorSound = GetComponent<Doorsound>();
        if (doorSound == null)
        {
            Debug.LogWarning("Doorsound script missing on this object!");
        }

    }
    void OnEnable()
    {
        doorCollider = this.GetComponent<BoxCollider2D>();
        animator = this.GetComponent<Animator>();
        _KeyCard1 = playerAttributes.KeyCard1;
        playerAttributes.OnKeyCard1Change += HandleKeyCard1Change;
    }
    void OnDisable()
    {
        playerAttributes.OnKeyCard1Change -= HandleKeyCard1Change;
    }
    void OnDestroy()
    {
        playerAttributes.OnKeyCard1Change -= HandleKeyCard1Change;
    }
    public void DoorOpen()
    {
        doorCollider.enabled = false;
        animator.GetComponent<Animator>().enabled = false;
        doorSound?.PlayDoorSound(); // Play Door sound

    }
    public void DoorClosed()
    {
        doorCollider.enabled = true;
        animator.GetComponent<Animator>().enabled = false;
        


    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _KeyCard1)
        {
            animator.GetComponent<Animator>().enabled = true;

        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _KeyCard1)
        {
            animator.GetComponent<Animator>().enabled = true;
        }
    }
    private void HandleKeyCard1Change(bool newValue)
    {
        _KeyCard1 = newValue;
    }
}

