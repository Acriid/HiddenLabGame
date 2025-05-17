using UnityEngine;

public class ResumeMovement : MonoBehaviour
{
    [SerializeField] GameObject Slime;
    private Movement slimeMovement;
    void OnEnable()
    {
        slimeMovement = Slime.GetComponent<Movement>();
    }
    void OnDisable()
    {
        if(slimeMovement != null && !slimeMovement.enabled)
        {
            slimeMovement.enabled = true;
        }
    }
}
