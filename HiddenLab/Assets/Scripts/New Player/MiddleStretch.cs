using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleScale : MonoBehaviour
{
    //finds both objects
    [SerializeField] private GameObject startobject;
    [SerializeField] private GameObject endobject;
    private Vector3 initialscale;
    // Start is called before the first frame update
    void Start()
    {
        initialscale = transform.localScale;
        updatetransformscale();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for changes in positions
       if (startobject.transform.hasChanged || endobject.transform.hasChanged)
       {
        updatetransformscale();
       } 
    }

    void updatetransformscale()
    {
        //Gets distance between objects
        float distance = Vector2.Distance(startobject.transform.position,endobject.transform.position);
        transform.localScale = new Vector3(initialscale.x,distance*2,initialscale.z);

        //Scales accordingly
        Vector3 middlepoint = (startobject.transform.position + endobject.transform.position)/2f;
        transform.position = middlepoint;

        //Rotates accordingly
        Vector3 rotationDirection = (endobject.transform.position - startobject.transform.position);
        transform.up = rotationDirection;
    }
}