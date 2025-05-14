using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour , IEnemyMovement
{
    public NavMeshAgent enemyagent {get; set;}
    public float enemySpeed {get; set;}
    public float IdleRadius = 5f;
    #region StateMachine Variables
    public EnemyStateMachine enemyStateMachine {get; set;}
    public EnemyIdleState enemyIdleState {get; set;}
    #endregion
    void Awake()
    {
        //Initialize Enemy States
        enemyStateMachine = new EnemyStateMachine();
        enemyIdleState = new EnemyIdleState(this , enemyStateMachine);
    }
    void Start()
    {
        //Set navmesh
        enemyagent = GetComponent<NavMeshAgent>();
        enemySpeed = 10f;
        enemyagent.updateRotation = false;
        enemyagent.updateUpAxis = false;
        //Initial state
        enemyStateMachine.Initialize(enemyIdleState);
    }
    void Update()
    {
        enemyStateMachine.currentEnemyState.UpdateState();
    }
    void FixedUpdate()
    {
        enemyStateMachine.currentEnemyState.FixedUpdateState();
    }
    public void MoveEnemy(Vector3 position)
    {
        enemyagent.SetDestination(position);
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        enemyStateMachine.currentEnemyState.AnimationTriggerEvent(triggerType);
    }
    public enum AnimationTriggerType
    {
        EnemyIdle,
        EnemMovement,
        EnemyChase
    }
}
