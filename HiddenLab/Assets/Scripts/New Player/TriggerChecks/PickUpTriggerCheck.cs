using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ItemTriggerCheck : MonoBehaviour
{
    private Player player;
    public  GameObject collisionObject;
    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            player.setisInPickuprange(true);
            collisionObject = collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            player.setisInPickuprange(false);
            collisionObject = null;
        }
    }
}
