using Unity.Collections;
using UnityEngine;

public class PlayerImpulseState : PlayerState
{
    
    public PlayerImpulseState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entered impulse state");
        if(player.GetSlimeDistance() > 1f)
        {
            player.MakeSlime1Kinematic(false);
            player.AdjustBreakForce(0f);
            player.ChangeSlime1LinearDamping(0.5f);
            player.Addimpulse(-player.directiontoSlime1());
            player.DestroySpringJoint2D();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdateState()
    {
        float distance = player.GetSlimeDistance();
        Vector2 Slime1Velocity = player.GetSlime1Velocity();
        base.FixedUpdateState();
    
        if(distance > 1f )
        {
            player.Slime2MoveDirection(player.directiontoSlime1());
        }
        else if(distance < 1f && Slime1Velocity.x < 1f && Slime1Velocity.y < 1f)
        {
            player.playerStateMachine.ChangeState(player.playerMoveState);
        }
    }
}

