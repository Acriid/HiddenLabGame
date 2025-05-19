using UnityEngine;

public class SaveTriggerCheck : MonoBehaviour
{
    private Player player;
    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SavePoint"))
        {
            player.setisInSaverange(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SavePoint"))
        {
            player.setisInSaverange(false);
        }
    }
}
