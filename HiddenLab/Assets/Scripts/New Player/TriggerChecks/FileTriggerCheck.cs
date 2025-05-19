using UnityEngine;

public class FileTriggerCheck : MonoBehaviour
{
    private Player player;
    void Awake()
    {
        player = this.GetComponentInParent<Player>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("File"))
        {
            player.setisInFilerange(true);
            player.SetFileObject(collision.gameObject, true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("File"))
        {

            player.SetFileObject(collision.gameObject, false);
            player.setisInFilerange(false);
        }
    }
}
