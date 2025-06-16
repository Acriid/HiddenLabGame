using UnityEngine;

public class FinalDoorOpen : MonoBehaviour, iDataPersistence
{
    [SerializeField] private Sprite OpenDoor;
    public void LoadData(GameData data)
    {
        if (data.ReactorOff)
        {
            this.GetComponent<SpriteRenderer>().sprite = OpenDoor;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    public void SaveData(ref GameData data)
    {

    }
}
