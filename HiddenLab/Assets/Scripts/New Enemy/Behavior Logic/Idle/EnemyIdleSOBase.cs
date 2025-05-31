//Title: Enhance Your Game: EASY Enemy Logic Using Scriptable Objects (State Machine PART 2) | Unity Tutorial
//Author: Sasquatch B Studios
//Date: 29 May 2025
//Code Version: 1
//Availability: https://www.youtube.com/watch?v=iOYo7flBUW4
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform enemyTransform;
    protected GameObject enemyGameObject;
    protected NavMeshAgent enemyAgent;
    protected GameObject playerGameObject;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        enemyGameObject = gameObject;
        this.enemy = enemy;
        enemyAgent = enemyGameObject.GetComponent<NavMeshAgent>();
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { }
    public virtual void DoUpdateLogic() { }
    public virtual void DoFixedUpdateLogic() { }
    public virtual void DoAnimationTriggerEventLogic() { }
}
