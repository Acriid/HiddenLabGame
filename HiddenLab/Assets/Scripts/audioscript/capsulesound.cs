using UnityEngine;


public class Ccapsulesound : MonoBehaviour
{
    [SerializeField] private string soundName = "Capsule";
   

    private void Start()
    {
        
        
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
            SoundEffectManager.Play("capsule");
        

    }
}