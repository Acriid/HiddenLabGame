using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlimeStreatch : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    private bool _isStreatched;
    private bool _addedImpulse;
    private float _impulseSpeed;
    [SerializeField] private GameObject Slime1;
    [SerializeField] private GameObject Slime2;
    private Rigidbody2D Slime1RB;
    private SpringJoint2D Slime1SJ;
    private Vector2 Slime1pos;
    private Rigidbody2D Slime2RB;
    private Vector2 Slime2pos;
    private float distanceBetween = 0f;
    private Vector2 directionToSlime1 = Vector2.zero;
    private bool isMoreLeft;
    private bool isMoreUp;

    private SlimeControls slimecontrols;
    private InputAction Streatch;
    void OnEnable()
    {
        if (Slime1SJ != null)
        {
            Slime1SJ = Slime1.GetComponent<SpringJoint2D>();
        }
        
        slimecontrols = new SlimeControls();
        Streatch = slimecontrols.Slime.Streatch;
        Streatch.Enable();
        Streatch.performed += StreatchSlime;
        playerAttributes.OnIsSplitChange += HandleIsStreachedChange;
        playerAttributes.OnAddedImpulseChange += HandleAddedImpulseChange;
        playerAttributes.OnImpulseSpeedChange += HandleImpulseSpeedChange;
        _addedImpulse = playerAttributes.AddedImpulse;
        _isStreatched = playerAttributes.IsStreached;
        _impulseSpeed = playerAttributes.ImpulseSpeed;
    }
    void OnDisable()
    {
        Streatch.Disable();
        Streatch.performed -= StreatchSlime;
        playerAttributes.OnIsSplitChange -= HandleIsStreachedChange;
        playerAttributes.OnAddedImpulseChange -= HandleAddedImpulseChange;
        playerAttributes.OnImpulseSpeedChange -= HandleImpulseSpeedChange;
    }
    void OnDestroy()
    {
        Streatch.Disable();
        Streatch.performed -= StreatchSlime;
        playerAttributes.OnIsSplitChange -= HandleIsStreachedChange;
        playerAttributes.OnAddedImpulseChange -= HandleAddedImpulseChange;
        playerAttributes.OnImpulseSpeedChange -= HandleImpulseSpeedChange;
    }
    void FixedUpdate()
    {
        distanceBetween = Vector2.Distance(Slime1RB.position,Slime2RB.position);
        directionToSlime1 = Slime1RB.position - Slime2RB.position;
        Slime1pos = Slime1RB.position;
        Slime2pos = Slime2RB.position;
        if (_addedImpulse)
        {

            Destroy(Slime1SJ);

            float deltaX = Slime1pos.x - Slime2pos.x;
            float deltaY = Slime1pos.y - Slime2pos.y;
            //Checks if isMoreLeft/isMoreRight is true/false then does the first thing if true and second if not
            bool xConflict = isMoreLeft? deltaX >= 0 : deltaX < 0;
            bool yConflict = isMoreUp? deltaY <=0 : deltaY > 0; 
            
            if ((xConflict || yConflict) && distanceBetween > 1f)
            {
                Slime2RB.linearVelocity = directionToSlime1 * 7.5f;
            }


        }
    }
    void StreatchSlime(InputAction.CallbackContext ctx)
    {
        //Gets the time and then checks if a specific time passed before running
        if ((_isStreatched) && (distanceBetween < 0.1f) && (Slime1RB.linearVelocity.x < 1f || Slime1RB.linearVelocity.y < 1f))
        {
            //Removes the second slime
            playerAttributes.RequestIsStreachedChange(false);
            Slime2.SetActive(false);
            playerAttributes.RequestAddedImpulseChange(false);
            Slime1RB.bodyType = RigidbodyType2D.Dynamic; 
            Slime1RB.linearDamping = 0;
        }
        if ((!_isStreatched) && (!_addedImpulse))
        {
            //Shows slime
            playerAttributes.RequestIsStreachedChange(true);
            Slime2.SetActive(true);           
            Slime2.transform.position = Slime1.transform.position;
            //Enables springjoints or makes springjoints incase its not there
            if (Slime1.GetComponent<SpringJoint2D>() == null)
            {
                //Slime1.AddComponent<SpringJoint2D>();
                Slime1SJ = Slime1.AddComponent<SpringJoint2D>();
                Slime1SJ.connectedBody = Slime2RB;
                Slime1SJ.distance = 0;
            }
            Slime1SJ.enabled = true;
            Slime1SJ.breakForce = 99999999;
            //Makes Slime1 stand still
            Slime1RB.bodyType = RigidbodyType2D.Kinematic; 
            Slime1RB.linearVelocity = Vector2.zero;
            

        }
        if ((_isStreatched) && (!_addedImpulse))
        {
            //Checks starting position of slime1 compared to slime 2
            isMoreLeft = Slime1pos.x < Slime2pos.x;
            isMoreUp = Slime1pos.y > Slime2pos.y;
            //Makes slime slingshot in direction of other slime
            Slime1RB.bodyType = RigidbodyType2D.Dynamic;
            Slime1RB.AddForce(-directionToSlime1 *_impulseSpeed, ForceMode2D.Impulse);
            playerAttributes.RequestAddedImpulseChange(true);
            Slime1SJ.breakForce = 0;
            Slime1RB.linearDamping = 0.25f;
        }



    }   
    private void HandleIsStreachedChange(bool newValue)
    {
        _isStreatched = newValue;
    } 
    private void HandleImpulseSpeedChange(float newValue)
    {
        _impulseSpeed = newValue;
    }
    private void HandleAddedImpulseChange(bool newValue)
    {
        _addedImpulse = newValue;
    }
}
