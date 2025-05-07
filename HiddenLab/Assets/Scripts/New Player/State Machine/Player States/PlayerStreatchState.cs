using UnityEngine;

public class PlayerStreatchState : PlayerState
{
    public PlayerStreatchState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
        //Make Player Stand Still
        player.MoveSlime(Vector2.zero);
        player.MoveSlime2(Vector2.zero);

    }

    public override void ExitState()
    {
        base.ExitState();

    }

    public override void UpdateState()
    {
        base.UpdateState();
        //Get movearrows value
        
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        //Move the player
    }
}
