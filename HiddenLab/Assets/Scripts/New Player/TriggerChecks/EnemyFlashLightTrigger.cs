using System.Collections;
using System.Threading;
using UnityEngine;

public class EnemyFlashLightTrigger : MonoBehaviour
{
    [SerializeField] private Enemy[] enemys;
    public GameObject fakeFlashlight;
    private float timer = 0f;
    private bool isChasing = false;
    void Update()
    {
        isChasing = false;
        foreach (Enemy enemy in enemys)
        {
            if (enemy.enemyStateMachine.currentEnemyState == enemy.enemyChaseState)
            {
                isChasing = true;
            }
        }
        if (isChasing)
        {
            timer += Time.deltaTime;
        }
        else if(fakeFlashlight.activeSelf)
        {
            fakeFlashlight.SetActive(false);
        }
        if (timer > 0.4f)
        {
            if (fakeFlashlight.activeSelf)
            {
                fakeFlashlight.SetActive(false);
            }
            else
            {
                fakeFlashlight.SetActive(true);
            }
            timer = 0f;
        }
    }
}
