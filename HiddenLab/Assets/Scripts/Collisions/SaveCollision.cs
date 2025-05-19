using UnityEngine;

public class SaveCollision : MonoBehaviour
{
    private Canvas canvas;
    void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.enabled = false;
        } 
    }
}
