using UnityEngine;

public class SaveOnEnter : MonoBehaviour
{
    void Start()
    {
        DataPersistanceManager.instance.SaveGame();
    }
}
