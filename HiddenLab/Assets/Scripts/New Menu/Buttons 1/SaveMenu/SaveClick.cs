using UnityEngine;

public class SaveClick : MonoBehaviour
{

    [SerializeField] private GameObject canvasSave;
    public void OnClick()
    {
        DataPersistanceManager.instance.SaveGame();
        canvasSave.SetActive(false);
        Time.timeScale = 1;
    }
}
