using UnityEngine;
using UnityEngine.InputSystem;

public class KeepCameraOnObject : MonoBehaviour
{
    SlimeControls slimeControls;
    private InputAction cPress;
    public GameObject ObjectTofollow;
    private Transform CameraTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        slimeControls = new SlimeControls();     
        CameraTransform = this.GetComponent<Transform>();  
    }
    private void OnEnable()
    {
        cPress = slimeControls.Slime.CameraMove;
        cPress.Enable();
    }
    private void OnDisable()
    {
        cPress.Disable();
    }
    // Update is called once per frame
    void Update()
    {
         CameraTransform.position = ObjectTofollow.transform.position - new Vector3(0f,0f,10f);

        
    }
}
