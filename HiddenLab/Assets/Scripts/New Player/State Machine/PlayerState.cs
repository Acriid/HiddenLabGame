using UnityEngine;

public class PlayerState 
{
   protected Player player;
   protected PlayerStateMachine playerStateMachine;
   /// Constructor to initialize the player and playerStateMachine
   protected PlayerState(Player player, PlayerStateMachine playerStateMachine)
   {
       this.player = player;
       this.playerStateMachine = playerStateMachine;
   }

   public virtual void EnterState() {}
   public virtual void ExitState() {}

   public virtual void UpdateState() {}
   public virtual void FixedUpdateState() {}
   public virtual void AnimationTriggerEvent(Player.AnimationTriggerType triggerType) {}
}
