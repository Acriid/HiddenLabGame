using UnityEngine;

public class Yin : Enemy
{
    [SerializeField] private PlayerAttributes playerAttributes;
    private bool InLight;
    void OnEnable()
    {
        InLight = playerAttributes.InLight;
        CanSeePlayer = InLight;
        playerAttributes.OnInLightChange += HandleInLightChange;
    }
    void OnDisable()
    {
        playerAttributes.OnInLightChange -= HandleInLightChange;
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
