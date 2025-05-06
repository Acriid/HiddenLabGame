using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovement
{
    Rigidbody2D SlimeRB {get; set;}
    SlimeControls slimecontrols {get; set;}
    InputAction MovementInput {get; set;}
    Vector2 MovementInputValue {get; set;}
    float _SlimeSpeed {get; set;}
    void MoveSlime();
}
