using UnityEngine;

public class ShowFile : MonoBehaviour
{

    public Player player;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.setisInFilerange(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.setisInFilerange(false);
        }
    }
}
