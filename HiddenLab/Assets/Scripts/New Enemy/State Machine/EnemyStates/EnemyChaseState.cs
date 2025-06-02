using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy , EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }
    public override void EnterState()
    {
        base.EnterState();
        enemy.EnemyChaseBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();
        enemy.EnemyChaseBaseInstance.DoExitLogic();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        enemy.EnemyChaseBaseInstance.DoUpdateLogic();       
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        enemy.EnemyChaseBaseInstance.DoFixedUpdateLogic();
    }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        enemy.EnemyChaseBaseInstance.DoAnimationTriggerEventLogic();
    }
}
