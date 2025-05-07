using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSplitState : PlayerState
{
    private SlimeControls slimeControls;
    private InputAction MoveArrows;
    private Vector2 MoveArrowsValue;
    private InputAction MoveWasd;
    private Vector2 MoveWasdValue;
    private InputAction SplitAction;

    public PlayerSplitState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        //Initialize controls
        slimeControls = new SlimeControls();
        slimeControls.Slime.Enable();
        MoveArrows = slimeControls.Slime.MoveArrows;
        MoveWasd = slimeControls.Slime.MoveWASD;
        SplitAction = slimeControls.Slime.Split;

        SplitAction.performed += OnSplitAction;
    }

    public override void ExitState()
    {
        base.ExitState();
        CleanupInputSystem();
        ClenupActions();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        //Get movearrows value
        MoveArrowsValue = MoveArrows.ReadValue<Vector2>();
        MoveWasdValue = MoveWasd.ReadValue<Vector2>();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        //Move the player
        player.MoveSlime(MoveArrowsValue);
        player.MoveSlime2(MoveWasdValue);
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
    private void ClenupActions()
    {
        SplitAction.performed -= OnSplitAction;
    }
    private void OnSplitAction(InputAction.CallbackContext ctx)
    {
        //Change state to move state
        player.playerStateMachine.ChangeState(player.playerMoveState);
    }
}
