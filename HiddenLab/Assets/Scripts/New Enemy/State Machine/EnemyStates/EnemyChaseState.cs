using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy , EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }
    public override void EnterState() 
    {
        base.EnterState();
        Debug.Log("In Chase State");

    }

    public override void ExitState() 
    {
        base.ExitState();
        Debug.Log("Left Chase State");
    }

    public override void UpdateState() 
    {
        base.UpdateState();
        if(enemy.CanSeePlayer == false)
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyIdleState);
        }
        enemy.MoveEnemy(enemy.player[1].transform.position);
        
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
