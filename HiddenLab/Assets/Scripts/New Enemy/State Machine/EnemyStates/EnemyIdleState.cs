
using UnityEngine;

//Reference properly
public class EnemyIdleState : EnemyState
{

    public EnemyIdleState(Enemy enemy , EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
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
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) 
    {
        base.AnimationTriggerEvent(triggerType);
    }


}
