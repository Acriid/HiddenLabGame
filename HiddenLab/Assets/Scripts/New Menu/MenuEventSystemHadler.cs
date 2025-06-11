//Title: Handle UI Like a Commerical Game (with just ONE script) - Unity Tutorial
//Author: Sasquatch B Studios
//Date: 27 May 2025
//Code Version: 1
//Availability: https://www.youtube.com/watch?v=0EsrYNAAEEY
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.InputSystem;

public class MenuEventSystemHadler : MonoBehaviour
{
    [Header("Player")]
    public GameObject playerHolder;
    [Header("References")]
    //Selectable variables are variables that can be modified by choosing from a set of options
    public List<Selectable> Selectables = new List<Selectable>();
    //Set first selected button
    [SerializeField] protected Selectable _firstSelected;
    //Keep Last Selected button
    protected Selectable _lastSelected;
    [Header("Controls")]
    [SerializeField] protected InputActionReference _navigateReference;
    [SerializeField] protected InputActionReference _clickAction;

    public virtual void OnEnable()
    {
        Debug.Log("Opened Menu");
        if (playerHolder != null)
        {
            playerHolder.SetActive(false);           
        }

        _navigateReference.action.performed += OnNavigate;
        _clickAction.action.performed += OnClickAction;
        StartCoroutine(SelectAfterDelay());
        Time.timeScale = 0;
    }
    public virtual void OnDisable()
    {
        if (this != null)
        {
            Debug.Log("Closed Menu");
            if (playerHolder != null)
            {  
                playerHolder.SetActive(true);
            }
            _navigateReference.action.performed -= OnNavigate;
            _clickAction.action.performed -= OnClickAction;
            Time.timeScale = 1;           
        }
    }
    public virtual void OnDestroy()
    {
        if (this != null)
        {
            Debug.Log("Closed Menu");
            if (playerHolder != null)
            {
                playerHolder.SetActive(true);                
            }
            _navigateReference.action.performed -= OnNavigate;
            _clickAction.action.performed -= OnClickAction;
            Time.timeScale = 1;           
        }
    }
    protected virtual void AddSelectionListners(Selectable selectable)
    {
        //Gets trigger and adds trigger if there is none
        EventTrigger trigger = selectable.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = selectable.gameObject.AddComponent<EventTrigger>();
        }
        //Add Select Event 
        EventTrigger.Entry SelectEntry = new EventTrigger.Entry
        {
            //Sets trigger type
            eventID = EventTriggerType.Select
        };
        SelectEntry.callback.AddListener(OnSelect);
        trigger.triggers.Add(SelectEntry);
        //Add Deselect Event
        EventTrigger.Entry DeselectEntry = new EventTrigger.Entry
        {
            //Sets trigger type
            eventID = EventTriggerType.Deselect
        };
        DeselectEntry.callback.AddListener(OnDeselect);
        trigger.triggers.Add(DeselectEntry);
    }

    protected virtual IEnumerator SelectAfterDelay()
    {
        //waits 0.5s
        yield return new WaitForSecondsRealtime(0.5f);
        //Selects first button that needs to be selected
        EventSystem.current.SetSelectedGameObject(_firstSelected.gameObject);
    }
    public void OnSelect(BaseEventData eventData)
    {
        //Get last selected button
        _lastSelected = eventData.selectedObject.GetComponent<Selectable>();
        //Todo Add animation if needed
    }
    public void OnDeselect(BaseEventData eventData)
    {
        //Stop animation if needed
    }
    protected virtual void OnNavigate(InputAction.CallbackContext ctx)
    {
        if (EventSystem.current.currentSelectedGameObject == null && _lastSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(_lastSelected.gameObject);
        }
    }
    protected virtual void OnClickAction(InputAction.CallbackContext ctx)
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        button.onClick.Invoke();
        
    }
}
