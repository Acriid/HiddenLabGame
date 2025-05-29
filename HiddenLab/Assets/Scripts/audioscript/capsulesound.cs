using UnityEngine;

public class capsulesound : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform player;
    public float triggerDistance = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < triggerDistance)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }

    }
}
