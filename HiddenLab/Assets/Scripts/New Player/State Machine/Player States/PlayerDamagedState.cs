using UnityEngine;

public class PlayerDamagedState : PlayerState
{
    public PlayerDamagedState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        
    }
    public override void EnterState()
    {
        base.EnterState();
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
        base.FixedUpdateState();
    }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
