using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerState
{

    private SlimeControls slimeControls;
    private InputAction MoveArrows;
    private Vector2 MoveArrowsValue;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entered move state");
        //Make Player Stand Still
        player.MoveSlime(Vector2.zero);
        player.MoveSlime2(Vector2.zero);
        //Initialize controls
        InitializeInputSystem();
        player.MakeSlime1Kinematic(false);
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
        if (MoveArrows != null)
        {
            MoveArrowsValue = MoveArrows.ReadValue<Vector2>();
            if (MoveArrowsValue.x > 0f)
            {
                player.SlimeRB.gameObject.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            }
            else if (MoveArrowsValue.x < 0f)
            {
                player.SlimeRB.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            }
        }
        
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        //Move the player
        player.MoveSlime(MoveArrowsValue);
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
    private void InitializeInputSystem()
    {
        if (slimeControls == null)
        {
            slimeControls = new SlimeControls();
            slimeControls.Slime.Enable();
        }
        if (MoveArrows == null)
        {
            MoveArrows = slimeControls.Slime.MoveArrows;
            MoveArrows.Enable();
        }
    }
    private void CleanupInputSystem()
    {
        if (slimeControls != null)
        {
            slimeControls.Slime.Disable();
            slimeControls.Dispose();
            slimeControls = null;
        }
        if (MoveArrows != null)
        {
            MoveArrows.Disable();
            MoveArrows = null;
        }
    }
}
