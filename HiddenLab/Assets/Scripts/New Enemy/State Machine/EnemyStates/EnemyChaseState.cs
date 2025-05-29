using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy , EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }
    public override void EnterState() 
    {
        base.EnterState();
    }

    public override void ExitState() 
    {
        base.ExitState();
        Debug.Log("Left Chase State");
    }

    public override void UpdateState() 
    {
        base.UpdateState();        
    }
    public override void FixedUpdateState() 
    {
        base.FixedUpdateState();
    }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) 
    {
        base.AnimationTriggerEvent(triggerType);
    }
}
