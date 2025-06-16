using UnityEngine;

public class KeyCard2Pickup : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    void Start()
    {
        //Makes keycard dissapear if already collected
        if(playerAttributes.KeyCard2)
        {
            this.gameObject.SetActive(false);  
        }
    }
}
