using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    private bool Forward;
    //0 is backwards 1 if forwards
    public Vector3[] scenePositions;
    public void OnClick()
    {
        Forward = playerAttributes.Forward;
        //Checks if forward or backwards and saves new position based on that information.
        if (Forward)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            DataPersistanceManager.instance.SceneSave(scenePositions[1]);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            DataPersistanceManager.instance.SceneSave(scenePositions[0]);
        }
    }
}