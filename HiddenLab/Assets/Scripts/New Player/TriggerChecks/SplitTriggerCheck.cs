using UnityEngine;

public class SplitTriggerCheck : MonoBehaviour
{
   public GameObject SplitSlime;
   private Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == SplitSlime)
        {
            player.setInRange(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == SplitSlime)
        {
            player.setInRange(false);
        }
    }
}
