using UnityEngine;
using UnityEngine.Tilemaps;

public class ChasmCollision : MonoBehaviour
{
    public PlayerAttributes playerAttributes;
    [SerializeField] private Player player;
    private TilemapCollider2D tilemapCollider2D;
    private float timer = 2f;
    void OnEnable()
    {
        playerAttributes.OnAddedImpulseChange += HandleAddedImpulseChange;
        tilemapCollider2D = this.gameObject.GetComponent<TilemapCollider2D>();
    }
    void OnDisable()
    {
        playerAttributes.OnAddedImpulseChange -= HandleAddedImpulseChange;
    }
    void OnDestroy()
    {
        playerAttributes.OnAddedImpulseChange -= HandleAddedImpulseChange;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (player.playerStateMachine.currentPlayerState != player.playerStretchState)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                collision.gameObject.transform.position = new Vector3(-18f, 0f, 0f);
                timer = 2f;
            }
        }
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
