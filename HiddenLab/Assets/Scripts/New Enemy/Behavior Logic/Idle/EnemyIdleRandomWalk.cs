//Title: Enhance Your Game: EASY Enemy Logic Using Scriptable Objects (State Machine PART 2) | Unity Tutorial
//Author: Sasquatch B Studios
//Date: 29 May 2025
//Code Version: 1
//Availability: https://www.youtube.com/watch?v=iOYo7flBUW4
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Walk", menuName = "Enemy Logic/Idle Logic/Random Walk")]
public class EnemyIdleRandomWalk : EnemyIdleSOBase
{
    private Vector3 targetPosition;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        //Gets initial point
        targetPosition = GetRandomPoint();
        targetPosition.z = 0f;
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
        //Moves enemy and gets new point when needed
        enemy.MoveEnemy(targetPosition);
        if ((enemy.transform.position - targetPosition).sqrMagnitude < 1.5f)
        {
            targetPosition = GetRandomPoint();
            targetPosition.z = 0f;
        }
        if(enemy.CanSeePlayer)
        {
            enemy.enemyStateMachine.ChangeState(enemy.enemyChaseState);
        }
    }
    public override void DoFixedUpdateLogic()
    {
        base.DoFixedUpdateLogic();
    }
    public override void DoAnimationTriggerEventLogic()
    {
        base.DoAnimationTriggerEventLogic();
    }
    private Vector3 GetRandomPoint()
    {
        //Return a random point in a unit circle
        Vector3 randomPosition = enemy.transform.position + (Vector3)Random.insideUnitCircle * enemy.IdleRadius;
        randomPosition.z = 0;
        Vector2 targetdirection = (randomPosition-enemy.enemytransform.position).normalized;
        //enemy.enemytransform.forward = targetdirection;
        return randomPosition;
    }
}
