using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour , IHealth , IMovement
{
    //Player Attributes
    public PlayerAttributes playerAttributes;
    //Implementation of the IHealth interface
    public int _CurrentHealth{get; set;}
    #region  Movement Variables
    public Rigidbody2D SlimeRB {get; set;}
    public float _SlimeSpeed {get; set;}
    #endregion

    #region State Machine Variables
    public PlayerStateMachine playerStateMachine{get; set;}
    public PlayerMoveState playerMoveState{get; set;}
    #endregion

    void Awake()
    {
        //Initialize the state machine and player states
        playerStateMachine = new PlayerStateMachine();
        playerMoveState = new PlayerMoveState(this, playerStateMachine);
        
    }
    void Start()
    {
        //Synce health with PlayerAttributes
        _CurrentHealth = playerAttributes.PlayerHealth;
        playerAttributes.OnPlayerHealthChange += HandleHealthChange;
        //Synce speed with PlayerAttributes
        _SlimeSpeed = playerAttributes.Slime1Speed;
        playerAttributes.OnSlime1SpeedChange += HandleSlime1SpeedChange;

        //Get RigidBody
        SlimeRB = this.GetComponent<Rigidbody2D>();

        //Initialize State Machine;
        playerStateMachine.Initialize(playerMoveState);
    }

    void Update()
    {
        playerStateMachine.currentPlayerState.UpdateState();
    }
    void FixedUpdate()
    {
        playerStateMachine.currentPlayerState.FixedUpdateState();
    }

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

    private void HandleSlime1SpeedChange(float newValue)
    {
        _SlimeSpeed = newValue;
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
        
    }
    private void CleanUpPlayerAttributes()
    {
        playerAttributes.OnPlayerHealthChange -= HandleHealthChange;
        playerAttributes.OnSlime1SpeedChange -= HandleSlime1SpeedChange;
    }
    #endregion
}
