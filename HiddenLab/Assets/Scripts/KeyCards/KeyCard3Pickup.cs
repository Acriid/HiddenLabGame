using UnityEngine;

public class KeyCard3Pickup : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    void Start()
    {
        //Makes keycard dissapear if already collected
        if(playerAttributes.KeyCard3)
        {
            this.gameObject.SetActive(false);  
        }
    }
}
