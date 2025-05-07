using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerState
{

    private SlimeControls slimeControls;
    private InputAction MoveArrows;
    private InputAction SplitAction;
    private Vector2 MoveArrowsValue;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        //Initialize controls
        slimeControls = new SlimeControls();
        slimeControls.Slime.Enable();
        MoveArrows = slimeControls.Slime.MoveArrows;
        SplitAction = slimeControls.Slime.Split;


        SplitAction.performed += OnSplitAction;
    }

    public override void ExitState()
    {
        base.ExitState();
        CleanupInputSystem();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        //Get movearrows value
        
        MoveArrowsValue = MoveArrows.ReadValue<Vector2>();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        //Move the player
        player.MoveSlime(MoveArrowsValue);
    }
   
    private void CleanupInputSystem()
    {
        if (slimeControls != null)
        {
            slimeControls.Slime.Disable();
            slimeControls.Dispose();
            slimeControls = null;
        }
    }

    private void OnSplitAction(InputAction.CallbackContext ctx)
    {
        player.playerStateMachine.ChangeState(player.playerSplitState);

    }
}
