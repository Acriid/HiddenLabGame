using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct", menuName = "Enemy Logic/Chase Logic/Direct")]
public class EnemyDirectChase : EnemyChaseSOBase
{
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
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
