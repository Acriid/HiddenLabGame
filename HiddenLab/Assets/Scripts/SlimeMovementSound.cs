using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the Audio Source
    public AudioClip movementSound; // movement sound clip


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
       audioSource = GetComponent<AudioSource>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0;

        if (isMoving ) // Check if the movement is detected and sound is not already playing
        {
            if(!audioSource.isPlaying)
            {
                audioSource.clip = movementSound;
                audioSource.Play();
            }

        }
        else
        {
            audioSource.Stop(); // Stop the sound when movement stops
        }

    }
}
