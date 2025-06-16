using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private GameObject EndMenu;
    void OnTriggerEnter2D(Collider2D collision)
    {
        EndMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
