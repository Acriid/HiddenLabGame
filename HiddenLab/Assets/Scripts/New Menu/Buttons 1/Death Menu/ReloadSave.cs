using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSave : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("DemoLevel");
    }
}
