using UnityEngine;

public class RemoveAfter3s : MonoBehaviour
{
    private float timer = 0f;
    [SerializeField] private float timetoreach;
    void OnEnable()
    {
        timer = 0f;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timetoreach)
        {
            this.gameObject.SetActive(false);
        }
    }
}
