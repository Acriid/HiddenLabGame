using UnityEngine;

public class PlayerInLight : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    public Enemy enemy;
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        playerAttributes.RequestInLightChange(true);
    }
    void OnTriggerExit(Collider other)
    {
        playerAttributes.RequestInLightChange(false);
    }

}
