using UnityEngine;

public class ShowInput : MonoBehaviour
{
    private GameObject canvas;

    void OnEnable()
    {
        canvas = GetComponentInChildren<Canvas>().gameObject;
        if (canvas == null)
        {
            Debug.Log("Canvas not found");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.SetActive(false);
        } 
    }
}
