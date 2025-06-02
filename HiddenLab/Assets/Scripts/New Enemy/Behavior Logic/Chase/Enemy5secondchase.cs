using System.Threading;
using TMPro;
using UnityEngine;

public class Enemy5secondchase : EnemyChaseSOBase
{
    private float timer = 5f;
    private float distance = 0f;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        timer = 5f;
        //Set speed for the enemy to take 5 seconds to get to player
        distance = Vector2.Distance(enemyAgent.transform.position, enemy.player[1].transform.position);
        enemy.SetEnemySpeed(distance / timer);
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
        timer -= Time.deltaTime;
        distance = Vector2.Distance(enemyAgent.transform.position, enemy.player[1].transform.position);
        if (timer > 0f)
        {
            enemy.SetEnemySpeed(distance / timer);
        }
        else
        {
            enemy.CanSeePlayer = false;
        }
        


        enemy.MoveEnemy(enemy.player[1].transform.position);
    }
    public override void DoFixedUpdateLogic()
    {
        base.DoFixedUpdateLogic();
    }
    public override void DoAnimationTriggerEventLogic()
    {
        base.DoAnimationTriggerEventLogic();
    }
}
