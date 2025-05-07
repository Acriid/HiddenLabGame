using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovement
{
    Rigidbody2D SlimeRB {get; set;}
    float _SlimeSpeed {get; set;}
    void MoveSlime(Vector2 movementValue);
    Rigidbody2D Slime2RB { get; set; }
    float _Slime2Speed { get; set; }
    void MoveSlime2(Vector2 movementValue);
}
