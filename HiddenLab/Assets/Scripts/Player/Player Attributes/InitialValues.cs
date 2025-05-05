using UnityEngine;

public class InitialValues : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    void Awake()
    {   
        //Initializes the values of the player
        //playerAttributes.PlayerHealth = 2;
        //playerAttributes.Slime1Speed = 10f;
        //playerAttributes.Slime2Speed = 10f;
        playerAttributes.IsStreached = false;
        playerAttributes.IsSplit = false;
        //playerAttributes.ImpulseSpeed = 5f;
        playerAttributes.AddedImpulse = false;
        playerAttributes.CarryItem = false;
        //Later on the values will be gotten from the save file.
    }

}
