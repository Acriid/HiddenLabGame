using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMovement 
{
    NavMeshAgent enemyagent {get; set;}
    float enemySpeed {get; set;}
    void MoveEnemy(Vector3 position);
}
