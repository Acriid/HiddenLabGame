using UnityEngine;

public class PlayerStateMachine 
{
   public PlayerState currentPlayerState {get; set;}
    //Constructor 
   public void Initialize(PlayerState startingState)
   {
       currentPlayerState = startingState;
       currentPlayerState.EnterState();
   }

    //State change method
   public void ChangeState(PlayerState newState)
   {
       currentPlayerState.ExitState();
       currentPlayerState = newState;
       currentPlayerState.EnterState();
   }
}
