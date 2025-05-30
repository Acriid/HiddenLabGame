using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerImpulseState : PlayerState
{
    float impulseclock = 0f;
    
    public PlayerImpulseState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        player.playerAttributes.RequestAddedImpulseChange(true);
        Debug.Log("Entered Impulse State");
        impulseclock = 0f;
        if (player.GetSlimeDistance() > 1f)
        {
            player.MakeSlime1Kinematic(false);
            player.AdjustBreakForce(0f);
            player.ChangeSlime1LinearDamping(4f);
            player.Addimpulse(-player.directiontoSlime1());
            player.DestroySpringJoint2D();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        player.EnableSlime2(false);
        player.playerAttributes.RequestAddedImpulseChange(false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        impulseclock += Time.deltaTime;
    }

    public override void FixedUpdateState()
    {
        float distance = player.GetSlimeDistance();
        Vector2 Slime1Velocity = player.GetSlime1Velocity();
        Vector2 Slime2Velocity = player.GetSlime2Velocity();
        base.FixedUpdateState();
    
        if(distance > 1f )
        {
            player.Slime2MoveDirection(player.directiontoSlime1());
        }
        else if(impulseclock > 1f && distance < 1f && Slime1Velocity.magnitude <= Slime2Velocity.magnitude)
        {
            player.playerStateMachine.ChangeState(player.playerMoveState);
        }
    }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}

