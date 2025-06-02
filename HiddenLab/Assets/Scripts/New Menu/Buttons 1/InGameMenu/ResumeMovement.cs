using UnityEngine;

public class ResumeMovement : MonoBehaviour
{
    [SerializeField] GameObject Slime;
    private Player player;
    void OnEnable()
    {
        player = Slime.GetComponent<Player>();
    }
    void OnDisable()
    {
        if(player != null && !player.enabled)
        {
            player.enabled = true;
        }
    }
}
