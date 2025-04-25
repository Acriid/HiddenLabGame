using UnityEngine;

public class Bouncy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.attachedRigidbody.linearVelocity = -collision.attachedRigidbody.linearVelocity;
        }
    }
}
