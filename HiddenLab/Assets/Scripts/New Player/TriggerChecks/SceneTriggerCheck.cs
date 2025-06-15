using UnityEngine;

public class SceneTriggerCheck : MonoBehaviour
{
    private Player player;
    void OnEnable()
    {
        player = GetComponentInParent<Player>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SceneCheck"))
        {
            player.SetInSceneChangerange(true);
        }
        
    }
    void OnTriggerExit2D(Collider2D collision)
    {   if (collision.CompareTag("SceneCheck"))
        {
            player.SetInSceneChangerange(false);
        }
        
    }
}