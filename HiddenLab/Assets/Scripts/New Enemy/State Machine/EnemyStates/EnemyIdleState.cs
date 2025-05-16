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
        //Gets initial point
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
        //Moves enemy and gets new point when needed
        enemy.MoveEnemy(targetPosition);
        if((enemy.transform.position - targetPosition).sqrMagnitude < 1.5f)
        {
            targetPosition = GetRandomPoint();
            targetPosition.z = 0f;
        }
        if(enemy.CanSeePlayer)
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyChaseState);
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
        //Return a random point in a unit circle
        Vector3 randomPosition = enemy.transform.position + (Vector3)Random.insideUnitCircle * enemy.IdleRadius;
        randomPosition.z = 0;
        Vector2 targetdirection = (randomPosition-enemy.enemytransform.position).normalized;
        enemy.enemytransform.up = targetdirection;
        return randomPosition;
    }
}
