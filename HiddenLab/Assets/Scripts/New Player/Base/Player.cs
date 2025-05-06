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
    public SlimeControls slimecontrols {get; set;}
    public InputAction MovementInput {get; set;}
    public Vector2 MovementInputValue {get; set;} = Vector2.zero;
    public float _SlimeSpeed {get; set;}
    #endregion
    void Start()
    {
        //Synce health with PlayerAttributes
        _CurrentHealth = playerAttributes.PlayerHealth;
        playerAttributes.OnPlayerHealthChange += HandleHealthChange;
        //Synce speed with PlayerAttributes
        _SlimeSpeed = playerAttributes.Slime1Speed;
        playerAttributes.OnSlime1SpeedChange += HandleSlime1SpeedChange;
        //Start InputActins
        if (slimecontrols == null)
        {
            slimecontrols = new SlimeControls();
        }
        //Enable InputActions
        MovementInput = slimecontrols.Slime.MoveArrows;
        slimecontrols.Slime.Enable();

        //Get RigidBody
        SlimeRB = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovementInputValue = MovementInput.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        MoveSlime();
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
    public void MoveSlime()
    {
        SlimeRB.linearVelocity = MovementInputValue * _SlimeSpeed;
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
        if (slimecontrols != null)
        {
            slimecontrols.Slime.Disable();
            slimecontrols.Dispose();
            slimecontrols = null;
        }
    }
    private void CleanUpPlayerAttributes()
    {
        playerAttributes.OnPlayerHealthChange -= HandleHealthChange;
        playerAttributes.OnSlime1SpeedChange -= HandleSlime1SpeedChange;
    }
    #endregion
}
