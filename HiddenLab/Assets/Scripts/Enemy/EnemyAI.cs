using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//Do proper refrencing for this code
public class EnemyAI : MonoBehaviour
{
    public float radius;
    //turns angle into slider
    [Range(0,360)]
    public float angle;
    public GameObject player;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public bool canSeePlayer;
    private NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        StartCoroutine(FieldOfViewRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(canSeePlayer)
        {
            agent.SetDestination(player.transform.position);
        }
        
    }

    private IEnumerator FieldOfViewRoutine()
    {
        //Makes it so that the courroutine is only called 5 times a second
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        //infinite loop
        while (true)
        {
            yield return wait;
            PlayerInView();
        }
    }

    private void PlayerInView()
    {
        Collider2D[] colliderchecks = Physics2D.OverlapCircleAll(this.transform.position,radius,playerMask);
        if(colliderchecks.Length != 0)
        {
            //Gets first instance
            Transform target = colliderchecks[0].transform;
            Vector2 targetDirection = (target.position - this.transform.position).normalized;

            if (Vector2.Angle(transform.up,targetDirection) < angle/2)
            {
                float targetDistance = Vector2.Distance(this.transform.position , target.position);
                if(!Physics2D.Raycast(this.transform.position,targetDirection,targetDistance, obstacleMask))
                {
                    canSeePlayer = true;
                    this.transform.up = targetDirection;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if(canSeePlayer)
        {
            canSeePlayer = false;
        }

    }
}
