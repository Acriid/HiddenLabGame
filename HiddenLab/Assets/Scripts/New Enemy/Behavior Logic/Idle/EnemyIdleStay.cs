using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Idle-Stay", menuName = "Enemy Logic/Idle Logic/Stay")]
public class EnemyIdleStay : EnemyIdleSOBase
{
    private float timer = 0f;
    bool wentback = false;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Idle");
        timer = 0f;
        //enemy.MoveEnemy(enemy.BasePosition);
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
        if (!wentback)
        {
            enemy.MoveEnemy(enemy.BasePosition);
            wentback = true;
        }
        
        if (enemy.CanSeePlayer)
        {
            timer += Time.deltaTime;
        }
        else if (timer != 0f)
        {
            timer = 0f;
        }
        if (timer > enemy.ChaseTimer)
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
}
