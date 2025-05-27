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
    [Header("References")]
    //Selectable variables are variables that can be modified by choosing from a set of options
    public List<Selectable> Selectables = new List<Selectable>();
    //Set first selected button
    [SerializeField] protected Selectable _firstSelected;
    //Keep Last Selected button
    protected Selectable _lastSelected;
    [SerializeField] protected InputActionReference _navigateReference;

    public virtual void OnEnable()
    {
        _navigateReference.action.performed += OnNavigate;
        StartCoroutine(SelectAfterDelay());
    }
    public virtual void OnDisable()
    {
        _navigateReference.action.performed -= OnNavigate;
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
        //waits a single frame
        yield return null;
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

    }
    protected virtual void OnNavigate(InputAction.CallbackContext ctx)
    {
        if (EventSystem.current.currentSelectedGameObject == null && _lastSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(_lastSelected.gameObject);
        }
    }
}
