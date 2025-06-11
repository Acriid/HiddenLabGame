using System.Threading;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-5s", menuName = "Enemy Logic/Chase Logic/5s")]
public class Enemy5secondchase : EnemyChaseSOBase
{
    private float timer = 5f;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Chasing");
        timer = 6.5f;
        //Set speed for the enemy to take 5 seconds to get to player
        enemy.ChasePlayer();
        enemy.SetEnemySpeed(enemy.targetdistance / timer);
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
        timer -= Time.deltaTime;
        if (timer > 0f)
        {
            enemy.SetEnemySpeed(enemy.targetdistance / timer);
        }
        else
        {
            enemy.CanSeePlayer = false;
        }



        enemy.ChasePlayer();
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
