using UnityEngine;

public class FinalDoorOpen : MonoBehaviour
{
    [SerializeField] private Sprite OpenDoor;
    [SerializeField] private PlayerAttributes playerAttributes;
    void Start()
    {
        Debug.Log(playerAttributes.ReactorOff);
        if (playerAttributes.ReactorOff)
        {
            this.GetComponent<SpriteRenderer>().sprite = OpenDoor;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
