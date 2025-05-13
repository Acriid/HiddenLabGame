using UnityEngine;

public class CameraFollow : MonoBehaviour , iDataPersistence
{
    [SerializeField] private GameObject Slime;
    [SerializeField] private PlayerAttributes playerAttributes;
    private Rigidbody2D SlimeRB;
    private Plane[] cameraPlanes;
    private float _cameraMovement;
    private Rigidbody2D cameraRB;
    public float UpDownRange = 4f;
    public float LeftRightRange = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadData(GameData data)
    {
        cameraRB.position = data.playerPosition - new Vector3(0f,0f,-10f);
    }
    public void SaveData(ref GameData data)
    {
        
    }
    void OnEnable()
    {
        SlimeRB = Slime.GetComponent<Rigidbody2D>();
        cameraRB = this.GetComponent<Rigidbody2D>();
        playerAttributes.OnSlime1SpeedChange += HandleCameraSpeedChange;
        _cameraMovement = playerAttributes.Slime1Speed;
    }
    void OnDisable()
    {
        playerAttributes.OnSlime1SpeedChange -= HandleCameraSpeedChange;
    }
    void OnDestroy()
    {
        playerAttributes.OnSlime1SpeedChange -= HandleCameraSpeedChange;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        cameraPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        //Variables
        Vector2 moveDirection = Vector2.zero;
        bool inupRange = InUpRange();
        bool indownRange = InDownRange();
        bool inleftRange = InLeftRange();
        bool inrightRange = InRightRange();
        bool isInCamera = IsInCamera();
        //Velocities

        if (inupRange)
            moveDirection.y = 1;
        else if (indownRange)
            moveDirection.y = -1;
        if (inleftRange)
            moveDirection.x = -1;
        else if (inrightRange)
            moveDirection.x = 1;

        cameraRB.linearVelocity = moveDirection.normalized * _cameraMovement;
        if(isInCamera)
        {
            if(inupRange && cameraRB.linearVelocity.y > SlimeRB.linearVelocity.y)
            {
                cameraRB.linearVelocityY = moveDirection.y * SlimeRB.linearVelocityY;
            }
            if(indownRange && cameraRB.linearVelocity.y < SlimeRB.linearVelocity.y)
            {
                cameraRB.linearVelocityY = -moveDirection.y * SlimeRB.linearVelocityY;
            }
            if(inleftRange && cameraRB.linearVelocity.x < SlimeRB.linearVelocity.x)
            {
                cameraRB.linearVelocityX = -moveDirection.x * SlimeRB.linearVelocityX;
            }
            if(inrightRange && cameraRB.linearVelocity.x > SlimeRB.linearVelocity.x)
            {
                cameraRB.linearVelocityX = moveDirection.x * SlimeRB.linearVelocityX;
            }
        }
        
    }
    bool IsInCamera()
    {
        foreach(Plane plane in cameraPlanes)
        {
            if(plane.GetDistanceToPoint(SlimeRB.transform.position) <0f)
            {
                return false;
            }
        }
        return true;
    }
    bool InUpRange()
    {
        if(cameraPlanes[3].GetDistanceToPoint(SlimeRB.transform.position) < UpDownRange)
        {
            return true;
        }
        return false;
    }
    bool InLeftRange()
    {
        if(cameraPlanes[0].GetDistanceToPoint(SlimeRB.transform.position) <LeftRightRange)
        {
            return true;
        }
        return false;
    }
    bool InRightRange()
    {
        if(cameraPlanes[1].GetDistanceToPoint(SlimeRB.transform.position) <LeftRightRange)
        {
            return true;
        }
        return false;
    }
    bool InDownRange()
    {
        if(cameraPlanes[2].GetDistanceToPoint(SlimeRB.transform.position) <UpDownRange)
        {
            return true;
        }
        return false;
    }
    void HandleCameraSpeedChange(float newValue)
    {
        _cameraMovement = newValue;
    }
}
