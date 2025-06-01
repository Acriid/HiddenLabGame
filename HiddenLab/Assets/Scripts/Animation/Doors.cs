using UnityEngine;

public class Doors : MonoBehaviour
{
    private BoxCollider2D doorCollider;
    private Animator animator;
    void Start()
    {
        doorCollider = this.GetComponent<BoxCollider2D>();
        animator = this.GetComponent<Animator>();
    }
    public void DoorOpen()
    {
        doorCollider.enabled = false;
        animator.GetComponent<Animator>().enabled = false;
    }
    public void DoorClosed()
    {
        doorCollider.enabled = true;
        animator.GetComponent<Animator>().enabled = false;
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.GetComponent<Animator>().enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.GetComponent<Animator>().enabled = true;
        }
    }
}
