using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CanSeePlayer : MonoBehaviour
{

    public Enemy enemy;
    public float radius;
    //turns angle into slider
    [Range(0,360)]
    public float angle;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    void Start()
    {
        StartCoroutine(FieldOfViewRoutine());
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
        Collider2D[] colliderchecks = Physics2D.OverlapCircleAll(enemy.enemytransform.position,radius,playerMask);
        if(colliderchecks.Length != 0)
        {
            //Gets first instance
            Transform target = colliderchecks[0].transform;
            Vector2 targetDirection = (target.position - enemy.enemytransform.position).normalized;

            if (Vector2.Angle(enemy.enemytransform.up,targetDirection) < angle/2)
            {
                float targetDistance = Vector2.Distance(enemy.enemytransform.position , target.position);
                if(!Physics2D.Raycast(enemy.enemytransform.position,targetDirection,targetDistance, obstacleMask))
                {
                    enemy.setCanSeePlayer(true);
                    //enemy.enemytransform.forward = targetDirection;
                }
                else
                {
                    enemy.setCanSeePlayer(false);
                }
            }
            else
            {
                enemy.setCanSeePlayer(false);
            }
        }
        else if(enemy.CanSeePlayer)
        {
            enemy.setCanSeePlayer(false);
        }

    }
}
