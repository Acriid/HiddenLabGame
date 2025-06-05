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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }


    }

}
