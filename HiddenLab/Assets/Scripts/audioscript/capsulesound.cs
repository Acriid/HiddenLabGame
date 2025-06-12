using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ccapsulesound : MonoBehaviour
{
    [SerializeField] private string soundName = "Capsule";
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        AudioClip clip = SoundEffectManager.GetClip(soundName);
        if (clip != null)
        {
            source.clip = clip;
            source.loop = true;
            source.playOnAwake = true;

            // 3D Spatial sound setup
            source.spatialBlend = 1f;       // Fully 3D
            source.minDistance = 2f;        // Full volume within this distance
            source.maxDistance = 15f;       // Fades out after this range
            source.rolloffMode = AudioRolloffMode.Linear;

            source.Play();
        }
    }
}