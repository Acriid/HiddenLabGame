using UnityEngine;

public class ShowInput : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    void OnEnable()
    {
        if (canvas == null)
        {
            Debug.Log("Canvas not found");
        }
        if (canvas.activeSelf)
        {
            canvas.SetActive(false);
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
