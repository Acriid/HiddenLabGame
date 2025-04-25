using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    [SerializeField] private GameObject text;
    private TextMeshProUGUI tmp;    
    void OnEnable()
    {
        tmp = text.GetComponent<TextMeshProUGUI>();
        playerAttributes.OnPlayerHealthChange += HandlePlayerHealthChange;
    }
    void OnDisable()
    {
        playerAttributes.OnPlayerHealthChange -= HandlePlayerHealthChange;
    }
    void OnDestroy()
    {
        playerAttributes.OnPlayerHealthChange -= HandlePlayerHealthChange;
    }
    void HandlePlayerHealthChange(int newValue)
    {
        tmp.text = "Hp: " + newValue;
    }
}
