using UnityEngine;
using UnityEngine.SceneManagement;

public class StartClick : MonoBehaviour,iDataPersistence
{
    private int ScenetoLoad;
    public void LoadData(GameData data)
    {
        ScenetoLoad = data.CurrentScene;
    }
    public void SaveData(ref GameData data)
    {

    }
    public void OnClick()
    {
        SceneManager.LoadScene(ScenetoLoad);
    }
}
