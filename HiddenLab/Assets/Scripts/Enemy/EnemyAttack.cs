using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //Initialize variables
    [SerializeField] private PlayerAttributes playerAttributes;
    [SerializeField] private float AttackTimer = 5f;
    [SerializeField] private GameObject canvus;
    private EnemyAI enemyAI;
    void Start()
    {
        enemyAI = this.GetComponent<EnemyAI>();
    }
    //Hurts the player
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AttackWait(AttackTimer));
            if(playerAttributes.PlayerHealth == 2)
            {
                playerAttributes.RequestPlayerHealthChange(1);
            }
            else
            {
                //Activates the death menu
                playerAttributes.RequestPlayerHealthChange(0);
                canvus.SetActive(true);
            }
            if(playerAttributes.IsSplit)
            {
                collision.collider.gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator AttackWait(float waitTime)
    {
        //Makes the enemy stop in place for a few seconds specfied by AttackTimer
        enemyAI.enabled = false;
        yield return new WaitForSeconds(waitTime);
        enemyAI.enabled = true;
    }
}
