using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovement
{
    Rigidbody2D SlimeRB {get; set;}
    float _SlimeSpeed {get; set;}
    void MoveSlime(Vector2 movementValue);
}
