using UnityEngine;

public class ShowFile : MonoBehaviour
{

    private Player player;
    public static GameObject file;
    void Awake()
    {
        file = this.gameObject;
        player = GetComponentInParent<Player>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("File"))
        {
            player.setisInFilerange(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("File"))
        {
            player.setisInFilerange(false);
        }
    }
}
