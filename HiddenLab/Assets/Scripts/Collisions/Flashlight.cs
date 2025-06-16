using UnityEngine;

public class Flashlight : MonoBehaviour , iDataPersistence
{
    public PlayerAttributes playerAttributes;
    public void LoadData(GameData data)
    {
        if (data.HasFlashlight)
        {
            this.gameObject.SetActive(false);
        }
       
    }
    public void SaveData(ref GameData data)
    {

    }

}
