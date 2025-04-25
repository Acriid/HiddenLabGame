using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpItemCollision : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    private bool _isStreatched;
    private bool _IsInTrigger = false;
    private bool _connected = false;
    private SlimeControls slimecontrols;
    private InputAction PickUp;
    private GameObject Slime;
    private Canvas canvas;
    private HingeJoint2D[] SlimeHJ;
    private HingeJoint2D SlimeHJSpecific;

    void OnEnable()
    {
        slimecontrols = new SlimeControls();
        PickUp = slimecontrols.Slime.Pickup;
        PickUp.Disable();
        canvas = this.GetComponentInChildren<Canvas>();
        PickUp.performed += OnInteract;
        playerAttributes.OnIsStreachedChange += HandleIsStreachedChange;
        _isStreatched = playerAttributes.IsStreached;
    }
    void OnDisable()
    {
        PickUp.Disable();
        PickUp.performed -= OnInteract;
        playerAttributes.OnIsStreachedChange -= HandleIsStreachedChange;
    }
    void OnDestroy()
    {
        PickUp.Disable();  
        PickUp.performed -= OnInteract;    
        playerAttributes.OnIsStreachedChange -= HandleIsStreachedChange;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            _IsInTrigger = true;
            PickUp.Enable();
            Slime = collision.gameObject;
            canvas.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _IsInTrigger = false;
            PickUp.Disable();
            canvas.enabled = false;
        }
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {

        if(!_connected && _IsInTrigger && !_isStreatched)
        {
            Slime.AddComponent<HingeJoint2D>();
            SlimeHJ = Slime.GetComponents<HingeJoint2D>();
            foreach (HingeJoint2D hinge in SlimeHJ)
            {
                if (hinge.connectedBody == null)
                {
                    SlimeHJSpecific = hinge;
                    break;
                }
            }

            SlimeHJSpecific.connectedBody = this.GetComponent<Rigidbody2D>();
            SlimeHJSpecific.enableCollision = true;
            playerAttributes.RequestCarryItemChange(true);
            _connected = true;
            playerAttributes.RequestCarryItemChange(true);
        }
        else if (_connected)
        {
            Destroy(SlimeHJSpecific);
            _connected = false;
            playerAttributes.RequestCarryItemChange(false);
        }
    }
    private void HandleIsStreachedChange(bool newValue)
    {
        _isStreatched = newValue;
    }
}
