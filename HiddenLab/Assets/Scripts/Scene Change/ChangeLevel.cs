using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] private int CurrentLevel;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }

    private void OnClick()
    {
        string NextScene;
        DataPersistanceManager.instance.SaveGame();
        switch (CurrentLevel)
        {
            case 1:
                NextScene = "Level 2";
                SceneManager.LoadScene(NextScene);
                break;
            case 2:
                NextScene = "Level 3";
                SceneManager.LoadScene(NextScene);
                break;
            case 3:
                NextScene = "Level 4";
                SceneManager.LoadScene(NextScene);
                break;
            case 4:
                NextScene = "Level 3";
                SceneManager.LoadScene(NextScene);
                break;
            case 5:
                NextScene = "Level 1";
                SceneManager.LoadScene(NextScene);
                break;
            case 6:
                NextScene = "Level 2";
                SceneManager.LoadScene(NextScene);
                break;
        }
        
    }
}
