using UnityEngine;
using UnityEngine.InputSystem;

public class SplitSlime : MonoBehaviour
{
    [SerializeField] private PlayerAttributes playerAttributes;
    private bool _isSplit;
    [SerializeField] private GameObject Slime1;
    [SerializeField] private GameObject Slime2;
    private Rigidbody2D Slime1RB;
    private Rigidbody2D Slime2RB;
    private float currentTime = 0f;

    private SlimeControls slimecontrols;
    private InputAction Split;
    void OnEnable()
    {
        Slime1RB = Slime1.GetComponent<Rigidbody2D>();
        Slime2RB = Slime2.GetComponent<Rigidbody2D>();

        slimecontrols = new SlimeControls();
        Split = slimecontrols.Slime.Split;
        Split.Enable();
        Split.performed += SlimeSplit;
        playerAttributes.OnIsSplitChange += HandleSlimeSplitChange;
    }
    void OnDisable()
    {
        Split.Disable();
        Split.performed -= SlimeSplit;
        playerAttributes.OnIsSplitChange -= HandleSlimeSplitChange;
    }
    void OnDestroy()
    {
        Split.Disable();
        Split.performed -= SlimeSplit;
        playerAttributes.OnIsSplitChange -= HandleSlimeSplitChange;
    }
    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
    }
    void SlimeSplit(InputAction.CallbackContext ctx)
    {
        
        //Checks if Z is pressed and if the slime is currently split
        if ((_isSplit) && (currentTime > 0.2f))
        {
            Slime2.SetActive(false);
            playerAttributes.RequestIsSplitChange(false);
            currentTime = 0f;
        }
        if ((!_isSplit) && (currentTime > 0.2f))
        {
            //Splits the slime
            playerAttributes.RequestIsSplitChange(true);
            Slime2.SetActive(true);
            currentTime = 0f;
            Slime2.transform.position = Slime1.transform.position;
        } 
    }
    void HandleSlimeSplitChange(bool newValue)
    {
        _isSplit = newValue;
    }
}
