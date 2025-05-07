using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStretchState : PlayerState
{
    private SlimeControls slimeControls;
    private InputAction MoveArrows;
    private Vector2 MoveArrowsValue;
    public PlayerStretchState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        player.EnableSlime2(true);
        //Make Player Stand Still
        player.MoveSlime(Vector2.zero);
        player.MoveSlime2(Vector2.zero);
        //Initialize controls
        slimeControls = new SlimeControls();
        slimeControls.Slime.Enable();
        MoveArrows = slimeControls.Slime.MoveArrows;

    }

    public override void ExitState()
    {
        base.ExitState();
        player.EnableSlime2(false);
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
        player.MoveSlime2(MoveArrowsValue);
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
}
