using System;
using System.Collections;
using UnityEngine;

public class WaitInMenu : MonoBehaviour
{
    [SerializeField] private GameObject FunnyObject;
    private StartPress startPress;
    void Start()
    {
        startPress = FunnyObject.GetComponent<StartPress>();
        StartCoroutine(MenuWait());
        
    }
    private IEnumerator MenuWait()
    {
        startPress.enabled = false;
        yield return new WaitForSeconds(0.1f);
        startPress.enabled = true;
        
    }
}
