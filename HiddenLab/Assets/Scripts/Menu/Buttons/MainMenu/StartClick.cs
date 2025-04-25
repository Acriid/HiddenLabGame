using UnityEngine;
using UnityEngine.SceneManagement;

public class StartClick : MonoBehaviour 
{
    public void OnClick()
    {

        SceneManager.LoadScene("DemoLevel");
    }
}
