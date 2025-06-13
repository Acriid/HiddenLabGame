using UnityEngine;

public class Check : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (this.GetComponent<Enemy>() == null)
        {
            Debug.Log("Nope");
        }
        else
        {
            Debug.Log("YAYY");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
