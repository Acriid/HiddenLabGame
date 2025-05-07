using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour, iDataPersistence
{

    //Values
    [SerializeField] private PlayerAttributes playerAttributes;
    //Slime 1 start
    public GameObject Slime1;
    Vector2 Slime1pos = Vector2.zero;
    Rigidbody2D Slime1RB;
    Vector2 Direction1 = Vector2.zero;
    private float _Slime1Speed;
    SpringJoint2D Slime1SJ;
    BoxCollider2D Slime1BC;
    //Slime 1 end
    //Slime 2 start
    public GameObject Slime2;
    Vector2 Slime2pos = Vector2.zero;
    Rigidbody2D Slime2RB;
    Vector2 Direction2 = Vector2.zero;
    private float _Slime2Speed;
    Vector2 directionToSlime1 = Vector2.zero;
    //SpringJoint2D Slime2SJ;
    //Slime 2 end
    //Need to call the script for the controls
    //Controls start
    private SlimeControls slimeControls;
    private InputAction movewasd;
    private InputAction movearrow;
    private InputAction Split;
    private InputAction Streatch;
    private float StreatchValue;
    private float SplitValue;
    //Controls end
    //Global checks start
    private bool _isSplit;
    private bool _carryItem;
    private float currentTime = 0;
    float distanceBetween = 0f;
    private bool _isStreatched;
    private bool isMoreLeft = false;
    private bool isMoreUp = false;
    //Global checks end
    //Other
    private float _impulseSpeed;
    private bool _addedImpulse;
    private int _playerHealth;
    //Other
    public void LoadData(GameData data)
    {
        Slime1.transform.position = data.playerPosition;
    }
    public void SaveData(ref GameData data)
    {
        data.playerPosition = Slime1.transform.position;
    }
    private void OnEnable()
    {   
        //Initialize things
        slimeControls = new SlimeControls();
        //Slime1
        Slime1RB = Slime1.GetComponent<Rigidbody2D>();
        Slime1BC = Slime1.GetComponent<BoxCollider2D>();
        //Try catch due to Slime1SJ being broken and giving an error when the code is rerun during play
        try
        {
            Slime1SJ = Slime1.GetComponent<SpringJoint2D>();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        
        //Slime2
        Slime2RB = Slime2.GetComponent<Rigidbody2D>();

        //Initializes the input actions
        movewasd = slimeControls.Slime.MoveWASD;
        movewasd.Enable();
        movearrow = slimeControls.Slime.MoveArrows;
        movearrow.Enable();
        Split = slimeControls.Slime.Split;
        Split.Enable();
        Streatch = slimeControls.Slime.Stretch;
        Streatch.Enable();

        //Subscribe to events
        playerAttributes.OnSlime1SpeedChange += HandleSlime1SpeedChange;
        playerAttributes.OnSlime2SpeedChange += HandleSlime2SpeedChange;
        playerAttributes.OnImpulseSpeedChange += HandleImpulseSpeedChange;
        playerAttributes.OnIsSplitChange += HandleIsSplitChange;
        playerAttributes.OnIsStreachedChange += HandleIsStreachedChange;
        playerAttributes.OnAddedImpulseChange += HandleAddedImpulseChange;
        playerAttributes.OnCarryItemChange += HandleCarryItemChange;
        playerAttributes.OnPlayerHealthChange += HandlePlayerHealthChange;
        //Initial values
        _Slime1Speed = playerAttributes.Slime1Speed;
        _Slime2Speed = playerAttributes.Slime2Speed;
        _impulseSpeed = playerAttributes.ImpulseSpeed;
        _isSplit = playerAttributes.IsSplit;
        _isStreatched = playerAttributes.IsStreached;
        _addedImpulse = playerAttributes.AddedImpulse;
        _carryItem = playerAttributes.CarryItem;
        _playerHealth = playerAttributes.PlayerHealth;

    }
    //Called when the object is disabled
    private void OnDisable()
    {
        //Disables the inputactions
        movewasd.Disable();
        movearrow.Disable();
        Split.Disable();
        Streatch.Disable();

        //Unsubscribe from events
        playerAttributes.OnSlime1SpeedChange -= HandleSlime1SpeedChange;
        playerAttributes.OnSlime2SpeedChange -= HandleSlime2SpeedChange;
        playerAttributes.OnImpulseSpeedChange -= HandleImpulseSpeedChange;
        playerAttributes.OnIsSplitChange -= HandleIsSplitChange;
        playerAttributes.OnIsStreachedChange -= HandleIsStreachedChange;
        playerAttributes.OnAddedImpulseChange -= HandleAddedImpulseChange;
        playerAttributes.OnCarryItemChange -= HandleCarryItemChange;

    }
    //Just in case the object gets destroyed and not disabled. Only for safety to prevent memory leeks
    private void OnDestroy()
    {
        //Disables the inputactions
        movewasd.Disable();
        movearrow.Disable();
        Split.Disable();
        Streatch.Disable();

        //Unsubscribe from events
        playerAttributes.OnSlime1SpeedChange -= HandleSlime1SpeedChange;
        playerAttributes.OnSlime2SpeedChange -= HandleSlime2SpeedChange;
        playerAttributes.OnImpulseSpeedChange -= HandleImpulseSpeedChange;
        playerAttributes.OnIsSplitChange -= HandleIsSplitChange;
        playerAttributes.OnIsStreachedChange -= HandleIsStreachedChange;
        playerAttributes.OnAddedImpulseChange -= HandleAddedImpulseChange;
        playerAttributes.OnCarryItemChange -= HandleCarryItemChange;
    }
    void Update()
    {
        Direction1 = movearrow.ReadValue<Vector2>();
        Direction2 = movewasd.ReadValue<Vector2>();

        StreatchValue = Streatch.ReadValue<float>();
        SplitValue = Split.ReadValue<float>();
    }
    //Called when anything to do with Physics happens
    void FixedUpdate()
    {
        //Updates the movement direction of the objects
        distanceBetween = Vector2.Distance(Slime1RB.position,Slime2RB.position);
        directionToSlime1 = Slime1RB.position - Slime2RB.position;
        Slime1pos = Slime1RB.position;
        Slime2pos = Slime2RB.position;
        //Splits the slime
        if (!_isStreatched && _playerHealth == 2)
        {
            SplitSlime();
        }
 
        //Streatches the slime
        if (!_isSplit && !_carryItem && _playerHealth == 2)
        {
            StreatchSlime();
        }

        //Checks for different conditions to allow/disallow different types of movement
        if (!_isStreatched)
        {
            Slime1MoveArrows();
        }
        if((_isStreatched) && (Direction1 != Vector2.zero) && (!_addedImpulse) && (!_isSplit))
        {
            Slime2MoveArrows();
        }
        if(_isSplit)
        {
            Slime2MoveWASD();
        }


        if (!_isSplit && Slime1SJ.IsDestroyed())
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

    void SplitSlime()
    {
        //Sets buffer
        currentTime += Time.deltaTime;
        //Checks if Z is pressed and if the slime is currently split
        if ((_isSplit) && (currentTime > 0.2f) && (!Mathf.Approximately(SplitValue,0f)))
        {
            Slime2RB.linearVelocity = directionToSlime1 * 7.5f;
            playerAttributes.RequestIsSplitChange(false);
            currentTime = 0f;
        }
        if ((!_isSplit) && (distanceBetween < 1f))
        {
            //UnSplits the slime
            Slime2.SetActive(false);
            currentTime = 0f; 
        }
        if ((!_isSplit) && (currentTime > 0.2f) && (!Mathf.Approximately(SplitValue,0f)))
        {
            //Splits the slime
            playerAttributes.RequestIsSplitChange(true);
            Slime2.SetActive(true);
            currentTime = 0f;
            Slime2.transform.position = Slime1.transform.position;
        } 
    }
    void StreatchSlime()
    {
        //Gets the time and then checks if a specific time passed before running
        if ((_isStreatched) && (distanceBetween < 0.1f) && (Slime1RB.linearVelocity.x < 1f || Slime1RB.linearVelocity.y < 1f) && (Mathf.Approximately(StreatchValue,0f)))
        {
            //Removes the second slime
            if (Slime1.GetComponent<SpringJoint2D>() != null)
            {
                Slime1SJ.enabled = false;
            }
            
            playerAttributes.RequestIsStreachedChange(false);
            Slime2.SetActive(false);
            playerAttributes.RequestAddedImpulseChange(false);
            Slime1RB.bodyType = RigidbodyType2D.Dynamic; 
            Slime1RB.linearDamping = 0;
            Streatch.Enable();
        }
        if ((!_isStreatched) && ((!_addedImpulse) && (!Mathf.Approximately(StreatchValue,0f))))
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
        if ((_isStreatched) && (!_addedImpulse) && (Mathf.Approximately(StreatchValue,0f)))
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
            Streatch.Disable();
        }

    }


    void Slime1MoveWASD()
    {
        Slime1RB.linearVelocity = Direction2 * _Slime1Speed;
    }

     void Slime1MoveArrows()
    {
        Slime1RB.linearVelocity = Direction1 * _Slime1Speed;
    }

     void Slime2MoveWASD()
    {
        Slime2RB.linearVelocity = Direction2 * _Slime2Speed;
    }
      void Slime2MoveArrows()
    {
        Slime2RB.linearVelocity = Direction1 * _Slime2Speed;
    }

    //Subscription events to get the values of the player.
    private void HandleSlime1SpeedChange(float newValue)
    {
        _Slime1Speed = newValue;
    }
    private void HandleSlime2SpeedChange(float newValue)
    {
        _Slime2Speed = newValue;
    }
    private void HandleIsStreachedChange(bool newValue)
    {
        _isStreatched = newValue;
    }
    private void HandleIsSplitChange(bool newValue)
    {
        _isSplit = newValue;
    }
    private void HandleImpulseSpeedChange(float newValue)
    {
        _impulseSpeed = newValue;
    }
    private void HandleAddedImpulseChange(bool newValue)
    {
        _addedImpulse = newValue;
    }
    private void HandleCarryItemChange(bool newValue)
    {
        _carryItem = newValue;
    }
    private void HandlePlayerHealthChange(int newValue)
    {
        _playerHealth = newValue;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            Streatch.Disable();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            Streatch.Enable();
        }
    }
}
