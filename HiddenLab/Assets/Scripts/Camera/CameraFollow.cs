using UnityEngine;

public class CameraFollow : MonoBehaviour , iDataPersistence
{
    [SerializeField] private GameObject Slime;
    [SerializeField] private PlayerAttributes playerAttributes;
    private Rigidbody2D SlimeRB;
    private Plane[] cameraPlanes;
    private float _cameraMovement;
    private Rigidbody2D cameraRB;
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

        Vector2 moveDirection = Vector2.zero;

        if (InUpRange())
            moveDirection.y = 1;
        else if (InDownRange())
            moveDirection.y = -1;
        if (InLeftRange())
            moveDirection.x = -1;
        else if (InRightRange())
            moveDirection.x = 1;
        cameraRB.linearVelocity = moveDirection * _cameraMovement;
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
        if(cameraPlanes[3].GetDistanceToPoint(SlimeRB.transform.position) < 4f)
        {
            return true;
        }
        return false;
    }
    bool InLeftRange()
    {
        if(cameraPlanes[0].GetDistanceToPoint(SlimeRB.transform.position) <10f)
        {
            return true;
        }
        return false;
    }
    bool InRightRange()
    {
        if(cameraPlanes[1].GetDistanceToPoint(SlimeRB.transform.position) <10f)
        {
            return true;
        }
        return false;
    }
    bool InDownRange()
    {
        if(cameraPlanes[2].GetDistanceToPoint(SlimeRB.transform.position) <4f)
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
