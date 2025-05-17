using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSave : MonoBehaviour
{
    [SerializeField] private GameObject canvasSave;
    public void OnClick()
    {
        canvasSave.SetActive(false);
        Time.timeScale = 1;
    }
}
