using UnityEngine;

public class ShowFile : MonoBehaviour
{

    Canvas[] canvasses;
    Canvas specificCanvas;
    void Awake()
    {
        canvasses = this.GetComponentsInChildren<Canvas>();
        foreach (Canvas canvas in canvasses)
        {
            if (canvas.name == "Button")
            {
                specificCanvas = canvas;
                break;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            specificCanvas.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            specificCanvas.enabled = false;
        }
    }
}
