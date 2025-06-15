using UnityEngine;
using UnityEngine.InputSystem;

public class Soundtrigger : MonoBehaviour
{
    public InputActionReference inputActionReference;
    bool isintrigger;
    private void OnEnable()
    {
        inputActionReference.action.performed += OnInputActionReference;

    }
    private void OnDisable()
    {
        inputActionReference.action.performed -= OnInputActionReference;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isintrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isintrigger = false;
        }
    }
    void OnInputActionReference(InputAction.CallbackContext ctx)
    {
        if (isintrigger)
        {
            SoundEffectManager.Play("pickup");
            Debug.Log("Playedsound");
        }
    }
}
