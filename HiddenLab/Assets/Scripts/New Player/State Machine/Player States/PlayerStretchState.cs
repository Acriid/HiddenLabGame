using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStretchState : PlayerState
{
    private SlimeControls slimeControls;
    private InputAction MoveArrows;
    private InputAction StretchAction;
    private Vector2 MoveArrowsValue;
    bool isStil;
    bool isOntop;
    bool isPressed;
    float zValue;
    public PlayerStretchState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entered stretch state");
        player.EnableSlime2(true);
        player.MakeSlime2OnSlime1();
        player.AddSpringJoint2D();
        player.MakeSlime1Kinematic(true);
        //Make Player Stand Still
        player.MoveSlime(Vector2.zero);
        player.MoveSlime2(Vector2.zero);
        //Initialize controls
        slimeControls = player.slimeControls;
        MoveArrows = slimeControls.Slime.MoveArrows;
        StretchAction = slimeControls.Slime.Stretch;

        player.middleSlime.SetActive(true);

    }

    public override void ExitState()
    {
        base.ExitState();
        //CleanupInputSystem();
        player.middleSlime.SetActive(false);

    }

    public override void UpdateState()
    {
        base.UpdateState();
        //Get movearrows value
        MoveArrowsValue = MoveArrows.ReadValue<Vector2>();
        isStil = Mathf.Approximately(player.GetSlime1Velocity().magnitude, 0f);
        isOntop = player.GetSlimeDistance() < 1f;
        zValue = StretchAction.ReadValue<float>();
        isPressed = zValue != 0f;
        updatetransformscale();


    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        //Move the player
        player.MoveSlime2(MoveArrowsValue);

        if (!isPressed)
        {
            player.playerStateMachine.ChangeState(player.playerMoveState);
        }
    }
    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
    private void CleanupInputSystem()
    {
        if (slimeControls != null)
        {
            slimeControls.Slime.Disable();
            slimeControls.Dispose();
            slimeControls = null;
        }
    }
    
    void updatetransformscale()
    {
        //Gets distance between objects
        float distance = Vector2.Distance(player.SlimeRB.transform.position,player.Slime2RB.transform.position);
        player.middleSlime.transform.localScale = new Vector3(player.InitialScale.x,distance,player.InitialScale.z);

        //Scales accordingly
        Vector3 middlepoint = (player.SlimeRB.transform.position + player.Slime2RB.transform.position)/2f;
        player.middleSlime.transform.position = middlepoint;

        //Rotates accordingly
        Vector3 rotationDirection = player.Slime2RB.transform.position - player.SlimeRB.transform.position;
        player.middleSlime.transform.up = rotationDirection;
    }
}
