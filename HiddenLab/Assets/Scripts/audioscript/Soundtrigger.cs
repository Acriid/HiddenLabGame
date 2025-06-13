using UnityEngine;

public class Soundtrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundEffectManager.Play("pickup");
        }
    }
}
