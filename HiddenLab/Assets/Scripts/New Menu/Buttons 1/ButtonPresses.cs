using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class StartPress : MonoBehaviour
{
    //Initialize variables
    private SlimeControls slimecontrols;
    private InputAction buttonclick;
    private InputAction moveArrows;
    private InputAction moveWasd;
    private Button[] buttons;
    private int currentIndex = 0;
    private int MaxNum = 0;
    private Coroutine enableCoroutine;
    [SerializeField] private float inputDelay = 0.1f;
    void OnEnable()
    {
        //Initialize variables
        slimecontrols = new SlimeControls();
        buttonclick = slimecontrols.UI.ButtonClick;
        moveArrows = slimecontrols.UI.MoveArrows;
        moveWasd = slimecontrols.UI.MoveWASD;
        //Subscribe to events
        moveArrows.performed += HandleButtonChange;
        moveWasd.performed += HandleButtonChange;
        buttonclick.performed += HandleButtonPress;
        //Get amount of buttons
        buttons = this.GetComponentsInChildren<Button>();
        MaxNum = buttons.Length - 1;
        buttons[0].Select();
        //starts coroutine
        enableCoroutine = StartCoroutine(DelayedInputEnable());
    }

    void OnDisable()
    {
        CleanupInputSystem();

    }
    void OnDestroy()
    {
        CleanupInputSystem();
    }
    private void HandleButtonPress(InputAction.CallbackContext ctx)
    {
        buttons[currentIndex].onClick.Invoke();
    }
    private void HandleButtonChange(InputAction.CallbackContext ctx)
    {
        Vector2 Values;
        Values = ctx.ReadValue<Vector2>();
        int x = Math.Sign(Values.x);
        int y = -Math.Sign(Values.y);
        if (x != 0)
        {
            currentIndex += x;  
        }
        if (y != 0)
        {
            currentIndex += y;
        }
        if (currentIndex > MaxNum)
        {
            currentIndex = 0;
        }
        if(currentIndex < 0)
        {
            currentIndex = MaxNum;
        }
        buttons[currentIndex].Select();
    }

     private void CleanupInputSystem()
    {
        //Checks if the couroutine is enabled
        if (enableCoroutine != null)
        {
            StopCoroutine(enableCoroutine);
            enableCoroutine = null;
        }

        //UnsubScribes from events
        if (moveArrows != null) 
        {
            moveArrows.performed -= HandleButtonChange;
        }
        if (moveWasd != null) 
        {
            moveWasd.performed -= HandleButtonChange;
        }
        if (buttonclick != null) 
        {
            buttonclick.performed -= HandleButtonPress;
        }
        //Properly Disposes the slimecontrols
        if (slimecontrols != null)
        {
            slimecontrols.UI.Disable();
            slimecontrols.Dispose();
            slimecontrols = null;
        }
    }

    private IEnumerator DelayedInputEnable()
    {
        yield return new WaitForSecondsRealtime(inputDelay);
        //Checks if component is enabled
        if (enabled) 
        {
            slimecontrols.UI.Enable();
        }
    }
}
