using UnityEngine;
using UnityEngine.InputSystem;


public class KeyCard1Pickup : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    void Start()
    {
        //Makes keycard dissapear if already collected
        if(playerAttributes.KeyCard1)
        {
            this.gameObject.SetActive(false);
        }

    }

}
