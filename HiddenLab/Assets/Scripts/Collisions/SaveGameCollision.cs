using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveGameCollision : MonoBehaviour
{
    //Initialize variables
    [SerializeField] private PlayerAttributes playerAttributes;
    [SerializeField] private GameObject canvasSave;
    private bool _isStreatched;
    private bool _IsInTrigger = false;
    private SlimeControls slimecontrols;
    private InputAction PickUp;
    private Canvas canvas;

    void OnEnable()
    {
        //enable controls and subscribe to events
        slimecontrols = new SlimeControls();
        PickUp = slimecontrols.Slime.Pickup;
        PickUp.Disable();
        canvas = this.GetComponentInChildren<Canvas>();
        PickUp.performed += SaveMenu;
        playerAttributes.OnIsStreachedChange += HandleIsStreachedChange;
        _isStreatched = playerAttributes.IsStreached;
    }
    void OnDisable()
    {
        //disable controls and unsubscribe from events
        PickUp = slimecontrols.Slime.Pickup;
        PickUp.Disable();
        PickUp.performed -= SaveMenu;
        playerAttributes.OnIsStreachedChange -= HandleIsStreachedChange;
    }
    void OnDestroy()
    {
        //disable controls and unsubscribe from events
        PickUp = slimecontrols.Slime.Pickup;
        PickUp.Disable();   
        PickUp.performed -= SaveMenu;
        playerAttributes.OnIsStreachedChange -= HandleIsStreachedChange;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Checks if player is in close proximity
        if (collision.CompareTag("Player"))
        {
            _IsInTrigger = true;
            PickUp.Enable();
            canvas.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        //Checks if player is in close proximity
        if (collision.CompareTag("Player"))
        {
            _IsInTrigger = false;
            PickUp.Disable();
            canvas.enabled = false;
        }
    }
    private void HandleIsStreachedChange(bool newValue)
    {
        _isStreatched = newValue;
    }
    private void SaveMenu(InputAction.CallbackContext ctx)
    {
        //Brings up save screen and heals the player
        if (!_isStreatched && _IsInTrigger)
        {
            canvasSave.SetActive(true);
            playerAttributes.RequestPlayerHealthChange(2);
            Time.timeScale = 0;
        } 
    }
}

