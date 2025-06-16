using UnityEngine;
using System.Collections;

public class Creaturesound : MonoBehaviour
{
    
    
    private bool isActive = false;

    void OnEnable()
    {
        isActive = true;
        SoundEffectManager.Play("CreatureSound");
    }

    void OnDisable()
    {
        isActive = false;
        SoundEffectManager.Stop("CreatureSound");
    }

   
    private void PlayRandomSound()
    {

        SoundEffectManager.Play("CreatureSound");  // Play chosen sound
    }

   
}