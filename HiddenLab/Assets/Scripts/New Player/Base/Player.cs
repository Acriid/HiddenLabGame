using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UIElements.Experimental;
using Unity.Mathematics;
using Unity.VisualScripting;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour , IHealth , IMovement , ITriggerChecks , iDataPersistence
{
    #region Menus
    public GameObject SaveMenu;
    public GameObject OptionsMenu;
    public GameObject DeathMenu;
    public GameObject PopupMenu;
    public GameObject SceneMenu;
    public TextMeshProUGUI popupmenuText;
    #endregion
    #region Player Attributes
    //Player Attributes
    public PlayerAttributes playerAttributes;
    private bool _isStreatched;
    private float _impulseSpeed;
    private ItemTriggerCheck itemTriggerCheck;
    #endregion
    private SpringJoint2D slimeSJ;
    private GameObject fileobject = null;
    //Implementation of the IHealth interface
    public int _CurrentHealth { get; set; }
    public float SlimeSize = 1.5f;
    private bool CanSplit;
    [SerializeField] private GameObject flashlight;
    #region  Movement Variables
    public Rigidbody2D SlimeRB { get; set; }
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
    [SerializeField] public GameObject middleSlime;
    public Vector3 InitialScale;
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
    private InputAction FlashLightAction;
    private InputAction SceneSwitchAction;
    //REMOVE LATER
    private InputAction KillAction;
    //REMOVE LATER
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

        InitialScale = middleSlime.transform.localScale;
        
    }
    void Start()
    {
        StartCoroutine(QuickWait());
        //PlayerAttributes
        InitializePlayerAttributes();

        //Controls
        ActionsInitialize();
        
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
    private void OnEnable()
    {
        StartCoroutine(QuickWait());
        //PlayerAttributes
        InitializePlayerAttributes();

        //Controls
        ActionsInitialize();

        itemTriggerCheck = GetComponentInChildren<ItemTriggerCheck>();

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
    private IEnumerator QuickWait()
    {
        yield return new WaitForSeconds(0.5f);
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
        DisableFlashLightAction();
    }
    void OnDestroy()
    {
        InitiateCleanup();
        DisableFlashLightAction();
    }
    #endregion
    #region Save/load
    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }
    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
        data.CurrentScene = SceneManager.GetActiveScene().buildIndex;
    }
    #endregion
    #region Health Functions
    public void Damage()
    {
        //Player can only take 2 hits total
        if (_CurrentHealth == 2)
        {
            playerAttributes.RequestPlayerHealthChange(1);
        }
        else if (_CurrentHealth < 2)
        {
            Death();
        }
        ClenupSlimeActions();
    }
    public int GetHealth()
    {
        return _CurrentHealth;
    }
    public void Death()
    {
        //TODO: show the death menu and pause the game
        DeathMenu.SetActive(true);
    }
    public void Heal()
    {
        //Only does something if player is hurt
        if (_CurrentHealth != 2)
        {
            playerAttributes.RequestPlayerHealthChange(2);
            ActionsInitialize();
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
            Debug.Log("ow");
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
        ClenupSlimeActions();
        CleanUpPlayerAttributes();
        if (slimeControls != null)
        {
            slimeControls.Slime.Disable();
            slimeControls.Dispose();
            slimeControls = null;
        }
    }
    private void ActionsInitialize()
    {
        if (slimeControls == null)
        {
            slimeControls = new SlimeControls();
            slimeControls.Slime.Enable();
        }
        if (OptionsAction == null)
        {
            OptionsAction = slimeControls.Slime.OpenMenu;
            OptionsAction.Enable();
            OptionsAction.performed += OnOptionsAction;
        }
        EnableStretchAction();
        if (playerAttributes.HasFlashlight)
        {
            EnableFlashLightAction();
        }
        if (playerAttributes.CanSplit)
        {
            EnableSplitAction();
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
        if (SaveAction != null)
        {
            SaveAction.Disable();
            SaveAction.performed -= onSaveAction;
            SaveAction = null;
        }
        if (FileAction != null)
        {
            FileAction.Disable();
            FileAction.performed -= onFileAction;
            FileAction = null;
        }
        DisableStretchAction();
        DisableSplitAction();
        if (OptionsAction != null)
        {
            OptionsAction.Disable();
            OptionsAction.performed -= OnOptionsAction;
            OptionsAction = null;
        }
        if (SceneSwitchAction != null)
        {
            SceneSwitchAction.Disable();
            SceneSwitchAction.performed -= OnSceneSwitchAction;
            SceneSwitchAction = null;
        }
        //remove later
            if (KillAction != null)
            {
                KillAction.Disable();
                KillAction.performed -= OnKillAction;
                KillAction = null;
            }
        //remove later

    }
    private void InitializePlayerAttributes()
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
        playerAttributes.OnImpulseSpeedChange += HandleImpulseSpeedChange;

        //FlashLight
        playerAttributes.OnFlashlightGet += HandleFlashLightGet;

        //Split
        playerAttributes.OnCanSplitChange += HandleSplitGet;
    }
    private void CleanUpPlayerAttributes()
    {
        playerAttributes.OnPlayerHealthChange -= HandleHealthChange;
        playerAttributes.OnSlime1SpeedChange -= HandleSlime1SpeedChange;
        playerAttributes.OnSlime2SpeedChange -= HandleSlime2SpeedChange;
        playerAttributes.OnImpulseSpeedChange -= HandleImpulseSpeedChange;
        playerAttributes.OnFlashlightGet -= HandleFlashLightGet;
        playerAttributes.OnCanSplitChange -= HandleSplitGet;
    }
    #region StretchAction enable/disable
    private void EnableStretchAction()
    {
        if (StretchAction == null)
        {
            StretchAction = slimeControls.Slime.Stretch;
            StretchAction.Enable();
            StretchAction.performed += OnStretchAction;
        }
    }
    private void DisableStretchAction()
    {
        if (StretchAction != null)
        {
            StretchAction.Disable();
            StretchAction.performed -= OnStretchAction;
            StretchAction = null;
        }
    }
    private void EnableFlashLightAction()
    {
        if (FlashLightAction == null)
        {
            FlashLightAction = slimeControls.Slime.FlashLight;
            FlashLightAction.performed += OnFlashLightAction;
            FlashLightAction.Enable();
        }
    }
    private void DisableFlashLightAction()
    {
        if (FlashLightAction != null)
        {
            FlashLightAction.Disable();
            FlashLightAction.performed -= OnFlashLightAction;
            FlashLightAction = null;
        }
    }
    private void EnableSplitAction()
    {
        if (SplitAction == null)
        {
            SplitAction = slimeControls.Slime.Split;
            SplitAction.Enable();
            SplitAction.performed += OnSplitAction;
        }
    }
    private void DisableSplitAction()
    {
        if (SplitAction != null)
        {
            SplitAction.Disable();
            SplitAction.performed -= OnSplitAction;
            SplitAction = null;
        }
    }
    #endregion
    #endregion
    #region Trigger checks
    public void setisInFilerange(bool value)
    {
        if (value)
        {
            //Disable stretch actions
            DisableStretchAction();
            //FileAction
            if (FileAction == null)
            {
                FileAction = slimeControls.Slime.File;
                FileAction.Enable();
                FileAction.performed += onFileAction;
            }

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
            if (PickupAction == null)
            {
                PickupAction = slimeControls.Slime.Pickup;
                PickupAction.Enable();
                PickupAction.performed += OnPickupAction;               
            }

        }
        else
        {
            //Enable stretch actions
            EnableStretchAction();
            //PickupAction
            if (PickupAction != null)
            {
                PickupAction.Disable();
                PickupAction.performed -= OnPickupAction;
                PickupAction = null;                
            }

        }
        isInPickuprange = value;
    }
    public void setisInSaverange(bool value)
    {
        if (value)
        {
            //Disable stretch actions
            DisableStretchAction();
            if (SaveAction == null)
            {
                SaveAction = slimeControls.Slime.Save;
                SaveAction.Enable();
                SaveAction.performed += onSaveAction;                
            }

        }
        else
        {
            //Enable stretch actions
            EnableStretchAction();
            if (SaveAction != null)
            {
                SaveAction.Disable();
                SaveAction.performed -= onSaveAction;
                SaveAction = null;               
            }

        }
        isInSaverange = value;
    }
    public void SetInSceneChangerange(bool value)
    {
        if (value)
        {
            DisableStretchAction();
            if (SceneSwitchAction == null)
            {
                SceneSwitchAction = slimeControls.Slime.SceneSwitch;
                SceneSwitchAction.Enable();
                SceneSwitchAction.performed += OnSceneSwitchAction;
            }
        }
        else
        {
            EnableStretchAction();
            if (SceneSwitchAction != null)
            {
                SceneSwitchAction.Disable();
                SceneSwitchAction.performed -= OnSceneSwitchAction;
                SceneSwitchAction = null;
            }
        }
    }
    #endregion
    #region FlashLight Action
    private void OnFlashLightAction(InputAction.CallbackContext ctx)
    {
        if (flashlight.activeSelf)
        {
            flashlight.SetActive(false);
            playerAttributes.RequestInLightChange(false);
        }
        else
        {
            flashlight.SetActive(true);
            playerAttributes.RequestInLightChange(true);
        }
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
        yield return new WaitForSeconds(1f); 
        SplitAction.Enable();
    }
    public void SetSlimeSize(float value)
    {
        Vector3 NewScale = Vector3.zero;
        NewScale.x = value;
        NewScale.y = value;
        NewScale.z = value;
        SlimeRB.gameObject.transform.localScale = NewScale;
        Slime2RB.gameObject.transform.localScale = NewScale;
    
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

    private IEnumerator CheckForBug()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
        }
        
    }
    #endregion
    #region PickUp Acion
    private void OnPickupAction(InputAction.CallbackContext ctx)
    {
        string text;
        if (itemTriggerCheck.collisionObject != null)
        {
            //KeyCard1 PickUp
            if (itemTriggerCheck.collisionObject.name == "KeyCard1")
            {
                playerAttributes.RequestKeyCard1Change(true);
                itemTriggerCheck.collisionObject.SetActive(false);
                itemTriggerCheck.collisionObject = null;
                text = "You got KeyCard1. Press esc to view current KeyCard.";
                popupmenuText.text = text;
                PopupMenu.SetActive(true);

            }
            //KeyCard2 PickUp
            else if (itemTriggerCheck.collisionObject.name == "KeyCard2")
            {
                playerAttributes.RequestKeyCard2Change(true);
                itemTriggerCheck.collisionObject.SetActive(false);
                itemTriggerCheck.collisionObject = null;
                text = "You got KeyCard2. Press esc to view current KeyCard.";
                popupmenuText.text = text;
                PopupMenu.SetActive(true);
            }
            //KeyCard3 PickUp
            else if (itemTriggerCheck.collisionObject.name == "KeyCard3")
            {
                playerAttributes.RequestKeyCard3Change(true);
                itemTriggerCheck.collisionObject.SetActive(false);
                itemTriggerCheck.collisionObject = null;
                text = "You got KeyCard3. Press esc to view current KeyCard.";
                popupmenuText.text = text;
                PopupMenu.SetActive(true);
            }
            //Flashlight Pickup
            else if (itemTriggerCheck.collisionObject.name == "FlashLight")
            {
                playerAttributes.RequestFlashLightGet(true);
                itemTriggerCheck.collisionObject.SetActive(false);
                itemTriggerCheck.collisionObject = null;
                text = "You got a flashlight. Press C to toggle.";
                popupmenuText.text = text;
                PopupMenu.SetActive(true);
            }
            else if (itemTriggerCheck.collisionObject.name == "Laser")
            {
                playerAttributes.RequestCanSplitChange(true);
                itemTriggerCheck.collisionObject = null;
                text = "You got cut in half. Press x to toggle second slime. Use WASD to move second slime.";
                popupmenuText.text = text;
                PopupMenu.SetActive(true);
            }
            else if (itemTriggerCheck.collisionObject.name == "Reactor")
            {
                itemTriggerCheck.collisionObject = null;
                playerAttributes.ReactorOff = true;
                text = "Reactor Turned off. Exit door in lvl 1 unlocked.";
                popupmenuText.text = text;
                PopupMenu.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Item Not Found");
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
                Time.timeScale = 1;
                
            }
            else
            {
                canvas[0].enabled = true;
                Time.timeScale = 0;
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
    #region Kill Action (Remove later)
    public void OnKillAction(InputAction.CallbackContext ctx)
    {
        Death();
    }
    #endregion
    #region Scene Switch
    public void OnSceneSwitchAction(InputAction.CallbackContext ctx)
    {
        SceneMenu.SetActive(true);
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
    public void HandleFlashLightGet(bool newValue)
    {
        if (newValue)
        {
            EnableFlashLightAction();
        }
    }
    public void HandleSplitGet(bool newValue)
    {
        if (newValue)
        {
            EnableSplitAction();
        }
    }
    #endregion
    #region Animation
    public void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        playerStateMachine.currentPlayerState.AnimationTriggerEvent(triggerType);
        Debug.Log("Animation Done");
    }
    public void Tester()
    {
        Debug.Log("Animation Done");
    }
    public enum AnimationTriggerType
    {
        PlayerIdle,
        PlayerMovement,
        PlayerMoveSound
    }
    #endregion
}
