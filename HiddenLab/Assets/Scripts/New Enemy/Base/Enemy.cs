using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour , IEnemyMovement , ITriggerCheck
{

    public Transform enemytransform;
    public GameObject[] player;
    public bool CanSeePlayer {get; set;}
    //
    public NavMeshAgent enemyagent {get; set;}
    public float enemySpeed {get; set;}
    public float IdleRadius = 5f;
    #region StateMachine Variables
    public EnemyStateMachine enemyStateMachine {get; set;}
    public EnemyIdleState enemyIdleState {get; set;}
    public EnemyChaseState enemyChaseState {get; set;}
    #endregion
    #region Awake/Start
    void Awake()
    {
        //Initialize Enemy States
        player = GameObject.FindGameObjectsWithTag("Player");
        enemyStateMachine = new EnemyStateMachine();
        enemyIdleState = new EnemyIdleState(this , enemyStateMachine);
        enemyChaseState = new EnemyChaseState(this , enemyStateMachine);
    }
    void Start()
    {
        enemytransform = GetComponent<Transform>();
        //Set navmesh
        enemyagent = GetComponent<NavMeshAgent>();
        enemySpeed = 10f;
        enemyagent.updateRotation = false;
        enemyagent.updateUpAxis = false;
        //Initial state
        enemyStateMachine.Initialize(enemyIdleState);
    }
    #endregion
    #region Update/FixedUpdate
    void Update()
    {
        enemyStateMachine.currentEnemyState.UpdateState();
    }
    void FixedUpdate()
    {
        enemyStateMachine.currentEnemyState.FixedUpdateState();
    }
    #endregion
    public void MoveEnemy(Vector3 position)
    {
        enemyagent.SetDestination(position);
    }

    public void setCanSeePlayer(bool newValue)
    {
        CanSeePlayer = newValue;
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
