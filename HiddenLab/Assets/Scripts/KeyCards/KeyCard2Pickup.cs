using UnityEngine;

public class KeyCard2Pickup : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    void Start()
    {
        //Makes keycard dissapear if already collected
        if(playerAttributes.KeyCard2)
        {
            this.gameObject.SetActive(false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Collects keycard
        if(collision.collider.CompareTag("Player"))
        {
            playerAttributes.RequestKeyCard2Change(true);
            this.gameObject.SetActive(false);
        }
    }
}
