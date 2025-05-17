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
    float waitTime = 0f;
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
    void Update()
    {
        waitTime += Time.deltaTime;
    }
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

        //Set movedirection
        if (inupRange)
        {
            moveDirection.y = 1;
        }     
        else if (indownRange)
        {
            moveDirection.y = -1;
        }
        if (inleftRange)
        {
            moveDirection.x = -1;
        }  
        else if (inrightRange)
        {
            moveDirection.x = 1;
        }

        //Set velocity
        cameraRB.linearVelocity = moveDirection.normalized * _cameraMovement;
       

        //Sync velocity with slimeRB
        if(isInCamera && waitTime < 3f)
        { 
            if(inupRange && (cameraRB.linearVelocity.y > SlimeRB.linearVelocity.y))
            {
                cameraRB.linearVelocityY = moveDirection.y * SlimeRB.linearVelocityY;
            }
            else if(indownRange && (cameraRB.linearVelocity.y < SlimeRB.linearVelocity.y))
            {
                cameraRB.linearVelocityY = -moveDirection.y * SlimeRB.linearVelocityY;
            }
            if(inleftRange && (cameraRB.linearVelocity.x < SlimeRB.linearVelocity.x))
            {
                cameraRB.linearVelocityX = -moveDirection.x * SlimeRB.linearVelocityX;
            }
            else if(inrightRange && (cameraRB.linearVelocity.x > SlimeRB.linearVelocity.x))
            {
                cameraRB.linearVelocityX = moveDirection.x * SlimeRB.linearVelocityX;
            }
        }
        if(!(inupRange || indownRange || inleftRange || inrightRange))
        {
            waitTime = 0f;
        }
    }
    bool IsInCamera()
    {
        bool result = true;
        foreach(Plane plane in cameraPlanes)
        {
            if(plane.GetDistanceToPoint(SlimeRB.transform.position) <0f)
            {
                result = false;
            }
        }
        return result;
    }
    bool InUpRange()
    {
        bool result = false;
        if(cameraPlanes[3].GetDistanceToPoint(SlimeRB.transform.position) < UpDownRange)
        {
            result = true;
        }
        return result;
    }
    bool InLeftRange()
    {
        bool result = false;
        if(cameraPlanes[0].GetDistanceToPoint(SlimeRB.transform.position) <LeftRightRange)
        {
            result = true;
        }
        return result;
    }
    bool InRightRange()
    {
        bool result = false;
        if(cameraPlanes[1].GetDistanceToPoint(SlimeRB.transform.position) <LeftRightRange)
        {
            result = true;
        }
        return result;
    }
    bool InDownRange()
    {
        bool result = false;
        if(cameraPlanes[2].GetDistanceToPoint(SlimeRB.transform.position) <UpDownRange)
        {
            result = true;
        }
        return result;
    }
    bool InCentre()
    {
        bool result = true;
        if(InUpRange() || InDownRange() || InLeftRange() || InRightRange())
        {
            result = false;
        }
        return result;
    }
    void HandleCameraSpeedChange(float newValue)
    {
        _cameraMovement = newValue;
    }
}
