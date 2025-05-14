using UnityEditor.Experimental.GraphView;
using UnityEngine;

//Reference properly
public class EnemyIdleState : EnemyState
{
    private Vector3 targetPosition;
    public EnemyIdleState(Enemy enemy , EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }


    public override void EnterState() 
    {
        base.EnterState();
        Debug.Log("Entered idle state");
        targetPosition = GetRandomPoint();
        targetPosition.z = 0f;
    }
    public override void ExitState() 
    {
        base.ExitState();
    }
    public override void UpdateState() 
    {
        base.UpdateState();

        enemy.MoveEnemy(targetPosition);
        if((enemy.transform.position - targetPosition).sqrMagnitude < 1.5f)
        {
            targetPosition = GetRandomPoint();
            targetPosition.z = 0f;
        }

    }
    public override void FixedUpdateState() 
    {
        base.FixedUpdateState();
    }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) 
    {
        base.AnimationTriggerEvent(triggerType);
    }

    private Vector3 GetRandomPoint()
    {
        return enemy.transform.position + (Vector3)Random.insideUnitCircle * enemy.IdleRadius;
    }
}
