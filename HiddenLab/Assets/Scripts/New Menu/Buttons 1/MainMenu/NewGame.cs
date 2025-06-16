using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void OnClick()
    {
        //Deletes save to make a new savefile
        DataPersistanceManager.instance.DeleteSave();
        SceneManager.LoadScene(1);
    }
}
