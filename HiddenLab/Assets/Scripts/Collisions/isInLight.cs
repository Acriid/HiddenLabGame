using System.Collections;
using UnityEngine;

public class isInLight : MonoBehaviour
{
    private bool InLight;
    [SerializeField] private PlayerAttributes playerAttributes;
    void OnEnable()
    {
        StartCoroutine(CheckifInLight());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InLight = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InLight = false;
        }
    }
    private IEnumerator CheckifInLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (InLight)
            {
                if (!playerAttributes.InLight)
                {
                    playerAttributes.RequestInLightChange(true);
                }
            }
        }
    }
}
