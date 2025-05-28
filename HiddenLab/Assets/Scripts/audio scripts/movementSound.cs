using UnityEngine;
using UnityEngine.InputSystem;

public class movementSound : MonoBehaviour
{
    public AudioClip MovementSound;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = MovementSound;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            audioSource.Play();
        }

        
    }
   
}
