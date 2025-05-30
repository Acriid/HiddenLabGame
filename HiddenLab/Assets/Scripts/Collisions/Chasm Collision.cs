using UnityEngine;
using UnityEngine.Tilemaps;

public class ChasmCollision : MonoBehaviour
{
    public PlayerAttributes playerAttributes;
    private TilemapCollider2D tilemapCollider2D;

    void OnEnable()
    {
        playerAttributes.OnAddedImpulseChange += HandleAddedImpulseChange;
        tilemapCollider2D = this.gameObject.GetComponent<TilemapCollider2D>();
    }
    void OnDisable()
    {
        playerAttributes.OnAddedImpulseChange -= HandleAddedImpulseChange;
    }
    private void HandleAddedImpulseChange(bool newValue)
    {
        if (newValue)
        {
            tilemapCollider2D.enabled = false;
        }
        else
        {
            tilemapCollider2D.enabled = true;
        }
    }
}
