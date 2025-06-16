using Unity.VisualScripting;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public int doorLevel;
    private Doorsound doorSound;
    private BoxCollider2D doorCollider;
    private Animator animator;
    public PlayerAttributes playerAttributes;
    private bool _KeyCard1;
    private bool _KeyCard2;
    private bool _KeyCard3;
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
        if (doorLevel == 1)
        {
            _KeyCard1 = playerAttributes.KeyCard1;
            playerAttributes.OnKeyCard1Change += HandleKeyCard1Change;
        }
        else if (doorLevel == 2)
        {
            _KeyCard2 = playerAttributes.KeyCard2;
            playerAttributes.OnKeyCard2Change += HandleKeyCard2Change;
        }
        else if (doorLevel == 3)
        {
            _KeyCard3 = playerAttributes.KeyCard3;
            playerAttributes.OnKeyCard3Change += HandleKeyCard3Change;
        }


    }
    void OnDisable()
    {
        if (doorLevel == 1)
        {
            playerAttributes.OnKeyCard1Change -= HandleKeyCard1Change;
        }
        else if (doorLevel == 2)
        {
            playerAttributes.OnKeyCard2Change -= HandleKeyCard2Change;
        }
        else if (doorLevel == 3)
        {
            playerAttributes.OnKeyCard3Change -= HandleKeyCard3Change;  
        }
    }
    void OnDestroy()
    {
        if (doorLevel == 1)
        {
            playerAttributes.OnKeyCard1Change -= HandleKeyCard1Change;
        }
        else if (doorLevel == 2)
        {
            playerAttributes.OnKeyCard2Change -= HandleKeyCard2Change;
        }
        else if (doorLevel == 3)
        {
            playerAttributes.OnKeyCard3Change -= HandleKeyCard3Change;  
        }
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
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            if (_KeyCard1 || _KeyCard2 || _KeyCard3 || collision.CompareTag("Enemy")) 
            {
                animator.GetComponent<Animator>().enabled = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            if (_KeyCard1 || _KeyCard2 || _KeyCard3 || collision.CompareTag("Enemy")) 
            {
                animator.GetComponent<Animator>().enabled = false;
            }
        }
    }
    private void HandleKeyCard1Change(bool newValue)
    {
        _KeyCard1 = newValue;
    }
    private void HandleKeyCard2Change(bool newValue)
    {
        _KeyCard2 = newValue;
    }
    private void HandleKeyCard3Change(bool newValue)
    {
        _KeyCard3 = newValue;
    }
}

