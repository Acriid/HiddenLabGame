using UnityEngine;

public class Yin : Enemy
{
    [SerializeField] private PlayerAttributes playerAttributes;
    private bool InLight;
    private AudioSource audioSource;

    void Awake()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        InLight = playerAttributes.InLight;
        CanSeePlayer = InLight;
        playerAttributes.OnInLightChange += HandleInLightChange;
        audioSource.Play();
    }
    void OnDisable()
    {
        playerAttributes.OnInLightChange -= HandleInLightChange;
        audioSource.Stop();
    }
    void OnDestroy()
    {
        playerAttributes.OnInLightChange -= HandleInLightChange;
    }
    private void HandleInLightChange(bool newValue)
    {
        InLight = newValue;
        CanSeePlayer = newValue;
    }
}
