using UnityEngine;

public class ResumeClick : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    public void OnClick()
    {
        Time.timeScale = 1;
        canvas.SetActive(false);
    }
}
