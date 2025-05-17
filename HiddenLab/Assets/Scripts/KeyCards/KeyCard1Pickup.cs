using UnityEngine;

public class KeyCard1Pickup : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    void Start()
    {
        //Makes keycard dissapear if already collected
        if(playerAttributes.KeyCard1)
        {
            this.gameObject.SetActive(false);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Collects keycard
        if(collision.collider.CompareTag("Player"))
        {
            playerAttributes.RequestKeyCard1Change(true);
            this.gameObject.SetActive(false);
        }
    }
}
