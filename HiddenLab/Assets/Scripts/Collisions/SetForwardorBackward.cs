using UnityEngine;

public class SetForwardorBackward : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    public int Forwardint;
    void OnTriggerEnter2D(Collider2D collision)
    {
            
        switch (Forwardint)
        {
            case 0:
                playerAttributes.RequestForwardChange(false);
                break;
            case 1:
                playerAttributes.RequestForwardChange(true);
                break;
        }
    
    }



}
