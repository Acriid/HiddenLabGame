using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UIElements.Experimental;
using Unity.Mathematics;
using Unity.VisualScripting;
using System.IO;


public class Player : MonoBehaviour , IHealth , IMovement , ITriggerChecks
{
    #region Menus
    public GameObject SaveMenu;
    public GameObject OptionsMenu;
    #endregion
    #region Player Attributes
    //Player Attributes
    public PlayerAttributes playerAttributes;
    private bool _isStreatched;
    private float _impulseSpeed;
    private bool _addedImpulse;
    #endregion
    private SpringJoint2D slimeSJ;
    private GameObject fileobject = null;
    //Implementation of the IHealth interface
    public int _CurrentHealth { get; set; }
    
    #region  Movement Variables
    public Rigidbody2D SlimeRB {get; set;}
    public float _SlimeSpeed {get; set;}

    public Rigidbody2D Slime2RB {get; set;}
    public float _Slime2Speed {get; set;}
    #endregion
    #region State Machine Variables
    public PlayerStateMachine playerStateMachine{get; set;}
    public PlayerMoveState playerMoveState{get; set;}
    public PlayerSplitState playerSplitState{get; set;}
    public PlayerStretchState playerStretchState{get; set;}
    public PlayerImpulseState playerImpulseState{get; set;}
    #endregion
    #region SlimeObjects
    private GameObject[] SlimeObjects;
    #endregion
    #region Trigger Checks
    public bool isInPickuprange { get; set; }
    public bool isInFilerange { get; set; }
    public bool isInSaverange { get; set; }
    #endregion
    #region Controls
    public SlimeControls slimeControls;
    private InputAction SplitAction;
    private InputAction StretchAction;
    private InputAction PickupAction;
    private InputAction FileAction;
    private InputAction SaveAction;
    private InputAction OptionsAction;
    #endregion
    #region Start/Awake
    void Awake()
    {
        //Initialize the state machine and player states
        playerStateMachine = new PlayerStateMachine();
        playerMoveState = new PlayerMoveState(this, playerStateMachine);
        playerSplitState = new PlayerSplitState(this, playerStateMachine);
        playerStretchState = new PlayerStretchState(this, playerStateMachine);
        playerImpulseState = new PlayerImpulseState(this, playerStateMachine);
        
    }
    void Start()
    {
        //Synce health with PlayerAttributes
        _CurrentHealth = playerAttributes.PlayerHealth;
        playerAttributes.OnPlayerHealthChange += HandleHealthChange;
        //Synce speed with PlayerAttributes
        _SlimeSpeed = playerAttributes.Slime1Speed;
        playerAttributes.OnSlime1SpeedChange += HandleSlime1SpeedChange;
        _Slime2Speed = playerAttributes.Slime2Speed;
        playerAttributes.OnSlime2SpeedChange += HandleSlime2SpeedChange;
        //Impulse
        _impulseSpeed = playerAttributes.ImpulseSpeed;
        _addedImpulse = playerAttributes.AddedImpulse;
        playerAttributes.OnAddedImpulseChange += HandleAddedImpulseChange;
        playerAttributes.OnImpulseSpeedChange += HandleImpulseSpeedChange;


        //Controls
        slimeControls = new SlimeControls();
        slimeControls.Slime.Enable();
        OptionsAction = slimeControls.Slime.OpenMenu;
        SplitAction = slimeControls.Slime.Split;
        EnableStretchAction();
        SplitAction.performed += OnSplitAction;
        OptionsAction.performed += OnOptionsAction;
        
        //Get RigidBody
        SlimeObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject slime in SlimeObjects)
        {
            if (slime.name == "Slime2")
            {
                Slime2RB = slime.GetComponent<Rigidbody2D>();
                Slime2RB.gameObject.SetActive(false);
            }
        }
        SlimeRB = this.GetComponent<Rigidbody2D>();

        

        //Slime SJ
        slimeSJ = null;
        //Initialize State Machine;
        playerStateMachine.Initialize(playerMoveState);
    }
    #endregion
    #region Update/FixedUpdate
    void Update()
    {
        playerStateMachine.currentPlayerState.UpdateState();
    }
    void FixedUpdate()
    {
        playerStateMachine.currentPlayerState.FixedUpdateState();
    }
    #endregion
    #region Disable and Destroy Functions
    //Disables the input system when the game is paused or the player is dead
    void OnDisable()
    {
        InitiateCleanup();
    }
    void OnDestroy()
    {
        InitiateCleanup();
    }
    #endregion
    #region Health Functions
    public void Damage()
    {
        //Player can only take 2 hits total
        if(_CurrentHealth == 2)
        {
            playerAttributes.RequestPlayerHealthChange(1);
        }
        else if(_CurrentHealth < 2)
        {
            Death();
        }
        ClenupSlimeActions();
    }

    public void Death()
    {
        //TODO: show the death menu and pause the game
    }
    public void Heal()
    {
        //Only does something if player is hurt
        if (_CurrentHealth != 2)
        {
            playerAttributes.RequestPlayerHealthChange(2);
            InitializeActions();
        }

    }

    private void HandleHealthChange(int newValue )
    {
        _CurrentHealth = newValue;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.CompareTag("Enemy"))
        {
            Vector2 direction = (collision.collider.gameObject.transform.position - SlimeRB.transform.position).normalized;
            Damage();
            EnemyImpulse(-direction);
        }
    }
    #endregion
    #region MovementFunctions
    public void MoveSlime(Vector2 movementValue)
    {
        SlimeRB.linearVelocity = movementValue * _SlimeSpeed;
    }
    public void MoveSlime2(Vector2 movementValue)
    {
        Slime2RB.linearVelocity = movementValue * _Slime2Speed;
    }
    private void HandleSlime1SpeedChange(float newValue)
    {
        _SlimeSpeed = newValue;
    }
    private void HandleSlime2SpeedChange(float newValue)
    {
        _Slime2Speed = newValue;
    }
    #endregion
    #region CleanUp of System / Initialize Actions
    private void InitiateCleanup()
    {
        CleanupInputSystem();
        CleanUpPlayerAttributes();
    }
    private void InitializeActions()
    {
        if(PickupAction == null)
        {
            PickupAction.Enable();
            PickupAction.performed += OnPickupAction;
        }
        EnableStretchAction();
        if(SplitAction == null)
        {
            SplitAction.Enable();
            SplitAction.performed += OnSplitAction;
        }
    }
    private void ClenupSlimeActions()
    {
        if (PickupAction != null)
        {
            PickupAction.Disable();
            PickupAction.performed -= OnPickupAction;
            PickupAction = null;
        }
        DisableStretchAction();
        if (SplitAction != null)
        {
            SplitAction.Disable();
            SplitAction.performed -= OnSplitAction;
            SplitAction = null;
        }
        if (OptionsAction != null)
        {
            OptionsAction.Disable();
            OptionsAction.performed -= OnOptionsAction;
            OptionsAction = null;
        }
    }
    private void CleanupInputSystem()
    {
        ClenupSlimeActions();
        if (slimeControls != null)
        {
            slimeControls.Slime.Disable();
            slimeControls.Dispose();
            slimeControls = null;
        }
    }
    private void CleanUpPlayerAttributes()
    {
        playerAttributes.OnPlayerHealthChange -= HandleHealthChange;
        playerAttributes.OnSlime1SpeedChange -= HandleSlime1SpeedChange;
        playerAttributes.OnSlime2SpeedChange -= HandleSlime2SpeedChange;
    }
    private void EnableStretchAction()
    {
        if (StretchAction == null)
        {
            StretchAction = slimeControls.Slime.Stretch;
            StretchAction.Enable();
            StretchAction.performed += OnStretchAction;
            StretchAction.canceled += OnStretchCancled;
        }
    }
    private void DisableStretchAction()
    {
        if (StretchAction != null)
        {
            StretchAction.Disable();
            StretchAction.performed -= OnStretchAction;
            StretchAction.canceled -= OnStretchCancled;
            StretchAction = null;
        }
    }
    #endregion
    #region Trigger checks
    public void setisInFilerange(bool value)
    {
        if (value)
        {
            //Disable stretch actions
            DisableStretchAction();
            //FileAction
            FileAction = slimeControls.Slime.File;
            FileAction.Enable();
            FileAction.performed += onFileAction;
        }
        else
        {
            //Enable stretch actions
            EnableStretchAction();
            //FileAction
            if (FileAction != null)
            {
                FileAction.Disable();
                FileAction.performed -= onFileAction;
                FileAction = null;
            }

        }
        isInFilerange = value;
    }
    public void setisInPickuprange(bool value)
    {
        if (value)
        {
            //Disable stretch actions
            DisableStretchAction();
            //PickupAction
            PickupAction = slimeControls.Slime.Pickup;
            PickupAction.Enable();
            PickupAction.performed += OnPickupAction;
        }
        else
        {
            //Enable stretch actions
            EnableStretchAction();
            //PickupAction
            PickupAction.Disable();
            PickupAction.performed -= OnPickupAction;
            PickupAction = null;
        }
        isInPickuprange = value;
    }
    public void setisInSaverange(bool value)
    {
        if (value)
        {
            //Disable stretch actions
            DisableStretchAction();
            SaveAction = slimeControls.Slime.Save;
            SaveAction.Enable();
            SaveAction.performed += onSaveAction;
        }
        else
        {
            //Enable stretch actions
            EnableStretchAction();
            SaveAction.Disable();
            SaveAction.performed -= onSaveAction;
        }
        isInSaverange = value;
    }
    #endregion
    #region Split Action
    private void OnSplitAction(InputAction.CallbackContext ctx)
    {
        // Disable the SplitAction to prevent multiple triggers
        SplitAction.Disable();

        // Switch to the other state
        if (playerStateMachine.currentPlayerState == playerMoveState)
        {
            playerStateMachine.ChangeState(playerSplitState);
        }
        else if (playerStateMachine.currentPlayerState == playerSplitState)
        {
            playerStateMachine.ChangeState(playerMoveState);
        }

        // Use the Player class to start the coroutine
        StartCoroutine(ReenableSplitAction());
    }

    private IEnumerator ReenableSplitAction()
    {
        yield return new WaitForSeconds(1f); // Adjust the delay as needed
        SplitAction.Enable();
    }
    #endregion
    #region Streatch Action
    private void OnStretchAction(InputAction.CallbackContext ctx)
    {
        CircleCollider2D[] circleCollider2D = this.GetComponentsInChildren<CircleCollider2D>();

        if (!_isStreatched)
            {
                foreach (CircleCollider2D circle in circleCollider2D)
                {
                    circle.enabled = false;
                }
                playerStateMachine.ChangeState(playerStretchState);
                _isStreatched = true;
            }
            else
            {
                foreach (CircleCollider2D circle in circleCollider2D)
                {
                    circle.enabled = true;
                }
                playerStateMachine.ChangeState(playerImpulseState);
                _isStreatched = false;
            }
    }
    private void OnStretchCancled(InputAction.CallbackContext ctx)
    {
        if (_isStreatched && playerStateMachine.currentPlayerState != playerImpulseState)
        {
            playerStateMachine.ChangeState(playerMoveState);
        } 
    }
    #endregion
    #region PickUp Acion
    private void OnPickupAction(InputAction.CallbackContext ctx)
    {
        HingeJoint2D specificHJtoadd = null;
        HingeJoint2D[] slimeHJs = this.GetComponents<HingeJoint2D>();
        if (slimeHJs.Length != 0)
        {
            foreach (HingeJoint2D specificHJ in slimeHJs)
            {
                Destroy(specificHJ);
            }
        }
        foreach (GameObject pickup in ItemTriggerCheck.pickupitems)
        {
            this.AddComponent<HingeJoint2D>();
            slimeHJs = this.GetComponents<HingeJoint2D>();
            foreach (HingeJoint2D specificHJ in slimeHJs)
            {
                if (specificHJ.connectedBody == null)
                {
                    specificHJtoadd = specificHJ;
                    break;
                }
            }
            specificHJtoadd.connectedBody = pickup.GetComponent<Rigidbody2D>();
            specificHJtoadd.enableCollision = true;
        }
    }
    #endregion
    #region File Action
    public void SetFileObject(GameObject gameObject,bool value)
    {
        Canvas canvas = gameObject.GetComponentInChildren<Canvas>();
        if (!value)
        {
            canvas.enabled = false;
            fileobject = null;
        }
        else
        {
            fileobject = gameObject;
        }
        
    }
    public void onFileAction(InputAction.CallbackContext ctx)
    {
        if (fileobject != null)
        {
            Canvas[] canvas = fileobject.GetComponentsInChildren<Canvas>();
            if (canvas[0].enabled == true)
            {
                canvas[0].enabled = false;
            }
            else
            {
                canvas[0].enabled = true;
            }
        }
        
    }
    #endregion
    #region Save Action
    public void onSaveAction(InputAction.CallbackContext ctx)
    {
        SaveMenu.SetActive(true);
        Time.timeScale = 0;
    }
    #endregion
    #region Options Action
    public void OnOptionsAction(InputAction.CallbackContext ctx)
    {
        OptionsMenu.SetActive(true);
        Time.timeScale = 0;
    }
    #endregion
    #region Joints
    public void AddSpringJoint2D()
    {
        //Adds springjoint to slime1
        if (slimeSJ == null)
        {
            slimeSJ = gameObject.AddComponent<SpringJoint2D>();
            slimeSJ.autoConfigureDistance = false;
            slimeSJ.autoConfigureConnectedAnchor = false;
            slimeSJ.connectedBody = Slime2RB;
            slimeSJ.distance = 0f;
            slimeSJ.dampingRatio = 0f;
            slimeSJ.frequency = 1f;
        }
    }
    public void AdjustBreakForce(float breakForce)
    {
        if (slimeSJ != null)
        {
            slimeSJ.breakForce = breakForce;
        }
    }
    public void DestroySpringJoint2D()
    {
        if (slimeSJ != null)
        {
            Destroy(slimeSJ);
            slimeSJ = null;
        }
    }
    #endregion
    #region SlimeAttributeChanges
    public void EnableSlime2(bool newvalue)
    {
        if(Slime2RB.gameObject.activeSelf != newvalue)
        {
            Slime2RB.gameObject.SetActive(newvalue);
        }
        else
        {
            return;
        }
    }
    public void MakeSlime1Kinematic(bool boolvalue)
    {
        if(boolvalue == true)
        {
            SlimeRB.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            SlimeRB.bodyType = RigidbodyType2D.Dynamic;
        }
        
    }
    public void MakeSlime2OnSlime1()
    {
        Slime2RB.transform.position = SlimeRB.transform.position;
    }
    #endregion
    #region Impulse
    public float GetSlimeDistance()
    {
        return Vector2.Distance(SlimeRB.transform.position, Slime2RB.transform.position);
    }
    public void Addimpulse(Vector2 direction)
    {
        SlimeRB.AddForce(direction * _impulseSpeed, ForceMode2D.Impulse);
        playerAttributes.RequestAddedImpulseChange(true);
        ChangeSlime1LinearDamping(0.25f);
    }
    public void EnemyImpulse(Vector2 direction)
    {
        SlimeRB.AddForce(direction * math.pow(_impulseSpeed,3), ForceMode2D.Impulse);
        playerStateMachine.ChangeState(playerImpulseState);
        ChangeSlime1LinearDamping(10f);
    }
    public Vector2 directiontoSlime1()
    {
        Vector2 directionToSlime1 = SlimeRB.transform.position - Slime2RB.transform.position;
        return directionToSlime1;
    }

    public void ChangeSlime1LinearDamping(float newValue)
    {
        SlimeRB.linearDamping = newValue;
    }
    private void HandleImpulseSpeedChange(float newValue)
    {
        _impulseSpeed = newValue;
    }
    private void HandleAddedImpulseChange(bool newValue)
    {
        _addedImpulse = newValue;
    }
    #endregion
    #region misc
    public void Slime2MoveDirection(Vector2 direction)
    {
        Slime2RB.linearVelocity = direction * 7.5f;
    }
    public Vector2 GetSlime1Velocity()
    {
        return SlimeRB.linearVelocity;
    }
    public Vector2 GetSlime2Velocity()
    {
        return Slime2RB.linearVelocity;
    }
    #endregion
    #region Animation
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        playerStateMachine.currentPlayerState.AnimationTriggerEvent(triggerType);
    }
    public enum AnimationTriggerType
    {
        PlayerIdle,
        PlayerMovement,
        PlayerMoveSound
    }
    #endregion
}
