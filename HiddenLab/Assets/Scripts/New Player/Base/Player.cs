using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class Player : MonoBehaviour , IHealth , IMovement , ITriggerChecks
{
    //Player Attributes
    public PlayerAttributes playerAttributes;
    //Implementation of the IHealth interface
    public int _CurrentHealth{get; set;}
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
    #endregion

    #region SlimeObjects
    private GameObject[] SlimeObjects;
    #endregion
    #region Trigger Checks
    public bool isInrange { get; set; }
    #endregion
    private SlimeControls slimeControls;
    private InputAction SplitAction;
    #region Start/Awake
    void Awake()
    {
        //Initialize the state machine and player states
        playerStateMachine = new PlayerStateMachine();
        playerMoveState = new PlayerMoveState(this, playerStateMachine);
        playerSplitState = new PlayerSplitState(this, playerStateMachine);
        
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

        //Controls
        slimeControls = new SlimeControls();
        slimeControls.Slime.Enable();
        SplitAction = slimeControls.Slime.Split;
        SplitAction.performed += OnSplitAction;
        
        //Get RigidBody
        SlimeObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject slime in SlimeObjects)
        {
            if (slime.name == "Slime2")
            {
                Slime2RB = slime.GetComponent<Rigidbody2D>();
            }
        }
        SlimeRB = this.GetComponent<Rigidbody2D>();

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
        else if(_CurrentHealth == 1)
        {
            Death();
        }
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
        }
    }

    private void HandleHealthChange(int newValue )
    {
        _CurrentHealth = newValue;
    }
    #endregion

    #region  MovementFunctions
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

    #region CleanUp of System
    private void InitiateCleanup()
    {
        CleanupInputSystem();
        CleanUpPlayerAttributes();
    }
    private void CleanupInputSystem()
    {
        if(SplitAction != null)
        {
            SplitAction.Disable();
            SplitAction.performed -= OnSplitAction;
            SplitAction = null;
        }
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
    #endregion
    
    #region Trigger checks
    public void setInRange(bool value)
    {
        isInrange = value;
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
}
