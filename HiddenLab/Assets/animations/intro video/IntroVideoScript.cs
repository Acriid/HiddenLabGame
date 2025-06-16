using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroVideoScript : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer.loopPointReached += NextScene;
    }
    private void NextScene(VideoPlayer vp)
    {
        StartCoroutine(Wait3Seconds());
    }
    private IEnumerator Wait3Seconds()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
