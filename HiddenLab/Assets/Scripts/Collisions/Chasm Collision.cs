using UnityEngine;
using UnityEngine.Tilemaps;

public class ChasmCollision : MonoBehaviour
{
    public Player player;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(player.playerStateMachine.currentPlayerState == player.playerStretchState)
        {
            this.GetComponent<TilemapCollider2D>().enabled = false;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if(player.playerStateMachine.currentPlayerState == player.playerStretchState)
        {
            this.GetComponent<TilemapCollider2D>().enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {

    }
}
