using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemyMovement, IEnemyTriggerCheck, IAttack
{
    #region Enemy Attack
    [SerializeField] public float AttackTimer { get; set; } = 5f;
    public float AttackRange { get; set; }
    #endregion
    #region Yin and Yang
    [SerializeField] public Vector2 BasePosition;
    [SerializeField] public float ChaseTimer;
    #endregion
    public Transform enemytransform;
    public GameObject[] player;
    public bool CanSeePlayer { get; set; }
    public NavMeshAgent enemyagent { get; set; }
    public float enemySpeed { get; set; } = 10f;
    public float IdleRadius = 5f;
    #region StateMachine Variables
    public EnemyStateMachine enemyStateMachine { get; set; }
    public EnemyIdleState enemyIdleState { get; set; }
    public EnemyChaseState enemyChaseState { get; set; }
    #endregion
    #region Scriptable Object Variables
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    #endregion
    #region Awake/Start
    void Awake()
    {
        //Makes a new instant of the object to not add anything to the scene
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        //Initialize Enemy States
        player = GameObject.FindGameObjectsWithTag("Player");
        enemyStateMachine = new EnemyStateMachine();
        enemyIdleState = new EnemyIdleState(this, enemyStateMachine);
        enemyChaseState = new EnemyChaseState(this, enemyStateMachine);
    }
    void Start()
    {
        enemytransform = GetComponent<Transform>();
        //Set navmesh
        enemyagent = GetComponent<NavMeshAgent>();
        enemyagent.speed = enemySpeed;
        enemyagent.updateRotation = false;
        enemyagent.updateUpAxis = false;
        //Initialize states
        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
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
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AttackWait(AttackTimer));
        }
    }
    public IEnumerator AttackWait(float waitTime)
    {
        enemyagent.speed = 0f;
        yield return new WaitForSeconds(waitTime);
        enemyagent.speed = enemySpeed;
    }
    public void SetEnemySpeed(float newValue)
    {
        enemySpeed = newValue;
        enemyagent.speed = enemySpeed;
    }
}
