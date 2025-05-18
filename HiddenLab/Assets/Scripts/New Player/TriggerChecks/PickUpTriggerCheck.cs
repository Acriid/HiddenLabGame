using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ItemTriggerCheck : MonoBehaviour
{
    private Player player;
    public static List<GameObject> pickupitems = new List<GameObject>();
    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            player.setisInPickuprange(true);
            pickupitems.Add(collision.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            player.setisInPickuprange(false);
            pickupitems.Remove(collision.gameObject);
        }
    }
}
