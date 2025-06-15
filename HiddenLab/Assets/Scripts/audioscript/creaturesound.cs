using UnityEngine;

public class Creaturesound : MonoBehaviour
{
    public AudioSource approachSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !approachSound.isPlaying)
        {
            approachSound.Play();
        }
    }
}