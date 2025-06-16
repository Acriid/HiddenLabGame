using System.Collections;
using UnityEngine;

public class EnemyFlashLightTrigger : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    public GameObject fakeFlashlight;
    void Update()
    {
        if (enemy.enemyStateMachine.currentEnemyState == enemy.enemyChaseState)
        {
            StartCoroutine(FlashFlashlight());
        }
    }
    private IEnumerator FlashFlashlight()
    {
        int counter = 0;
        while (counter < 4)
        {
            if (counter % 2 != 0)
            {
                fakeFlashlight.SetActive(true);
            }
            else
            {
                fakeFlashlight.SetActive(false);
            }
            yield return new WaitForSecondsRealtime(0.2f);
            counter++;
        }

    }
}
