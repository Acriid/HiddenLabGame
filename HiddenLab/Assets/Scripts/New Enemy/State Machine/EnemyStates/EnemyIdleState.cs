
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
        enemy.EnemyIdleBaseInstance.DoEnterLogic();
    }
    public override void ExitState()
    {
        base.ExitState();
        enemy.EnemyIdleBaseInstance.DoExitLogic();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        enemy.EnemyIdleBaseInstance.DoUpdateLogic();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        enemy.EnemyIdleBaseInstance.DoFixedUpdateLogic();
    }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        enemy.EnemyIdleBaseInstance.DoAnimationTriggerEventLogic();
    }


}
