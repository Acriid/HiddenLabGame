using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Collections;
using JetBrains.Annotations;

public class Filepopup : MonoBehaviour
{
    [SerializeField] GameObject objecttoshow;

    [SerializeField] Button _filename;
    [SerializeField] Text _popupText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Init(Transform FIle, string popupMessage, Action action)
    {
        _popupText.text = popupMessage;

        transform.SetParent(FIle.transform);
        transform.localScale = Vector3.one;

        _filename.onClick.AddListener(() =>
        {
            GameObject.Destroy(this.gameObject);
        });

    }

    public GameObject filepopup;

    public void PopupImage()
    {
        // if text is showing,
        //then (IS SHOWING) hide the text
        //else (IS NOT SHOWING) show the text
        filepopup.SetActive(!filepopup.activeInHierarchy);
        print("I HAVE BEEN CLICKED");

        //objecttoshow.SetActive(true);

    }
    public void HidepopupT()
    {

        objecttoshow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
