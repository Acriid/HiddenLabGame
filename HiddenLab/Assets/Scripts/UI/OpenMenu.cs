using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenMenu : MonoBehaviour
{
    private GameObject Child;
    private SlimeControls slimecontrols;
    public InputAction openMenu;
    void OnEnable()
    {
        //Enables and subscribes to events
        slimecontrols = new SlimeControls();
        openMenu = slimecontrols.Slime.OpenMenu;
        openMenu.Enable();
        Child = this.transform.GetChild(0).gameObject;
        openMenu.performed += MenuShow;
    }
    void OnDisable()
    {
        //Disables controls and unsubscribes
        openMenu.Disable();
        openMenu.performed -= MenuShow;
    }
    void OnDestroy()
    {
        //Disables controls and unsubscribes
        openMenu.Disable();
        openMenu.performed -= MenuShow;
    }

    //Shows the menu
    void MenuShow(InputAction.CallbackContext ctx)
    {
        //Checks if canvas is active
        if(Child.activeSelf)
        {
            Child.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            Child.SetActive(true);
            Time.timeScale = 0;

        }
    }
}
