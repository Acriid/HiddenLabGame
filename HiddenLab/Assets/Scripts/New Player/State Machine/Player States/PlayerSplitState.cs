using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSplitState : PlayerState
{
    private SlimeControls slimeControls;
    private InputAction MoveArrows;
    private Vector2 MoveArrowsValue;
    private InputAction MoveWasd;
    private Vector2 MoveWasdValue;

    public PlayerSplitState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        player.EnableSlime2(true);
        //Make Player Stand Still
        player.MakeSlime2OnSlime1();
        player.MoveSlime(Vector2.zero);
        player.MoveSlime2(Vector2.zero);

        player.SetSlimeSize(player.SlimeSize/2);
        //Initialize controls
        slimeControls = new SlimeControls();
        slimeControls.Slime.Enable();
        MoveArrows = slimeControls.Slime.MoveArrows;
        MoveWasd = slimeControls.Slime.MoveWASD;

    }

    public override void ExitState()
    {
        base.ExitState();
        player.SetSlimeSize(player.SlimeSize);
        player.EnableSlime2(false);
        CleanupInputSystem();
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
    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
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
