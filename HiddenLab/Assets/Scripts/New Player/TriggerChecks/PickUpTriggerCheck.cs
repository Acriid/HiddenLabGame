using UnityEngine;

public class ItemTriggerCheck : MonoBehaviour
{
   public GameObject SplitSlime;
   private Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pickup"))
        {
            player.setisInPickuprange(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Pickup"))
        {
            player.setisInPickuprange(false);
        }
    }
}
